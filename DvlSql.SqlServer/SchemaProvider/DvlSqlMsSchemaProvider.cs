using System.Data;
using DvlSql.Expressions;
using static DvlSql.MsExpressionHelpers;
using static DvlSql.ExpressionHelpers;

namespace DvlSql.SqlServer;

partial class DvlSqlMs
{
    private static SqlDbType GetSqlDbType(string dataType) =>
        dataType.Trim().ToLowerInvariant() switch
        {
            "tinyint" => SqlDbType.TinyInt,
            "smallint" => SqlDbType.SmallInt,
            "int" => SqlDbType.Int,
            "bigint" => SqlDbType.BigInt,
            "decimal" or "numeric" => SqlDbType.Decimal,
            "real" => SqlDbType.Real,
            "float" => SqlDbType.Float,
            "money" => SqlDbType.Money,
            "smallmoney" => SqlDbType.SmallMoney,
            "bit" => SqlDbType.Bit,

            "char" => SqlDbType.Char,
            "varchar" => SqlDbType.VarChar,
            "text" => SqlDbType.Text,
            "nchar" => SqlDbType.NChar,
            "nvarchar" => SqlDbType.NVarChar,
            "ntext" => SqlDbType.NText,

            "date" => SqlDbType.Date,
            "time" => SqlDbType.Time,
            "smalldatetime" => SqlDbType.SmallDateTime,
            "datetime" => SqlDbType.DateTime,
            "datetime2" => SqlDbType.DateTime2,
            "datetimeoffset" => SqlDbType.DateTimeOffset,

            "binary" => SqlDbType.Binary,
            "varbinary" => SqlDbType.VarBinary,
            "image" => SqlDbType.Image,
            "timestamp" or "rowversion" => SqlDbType.Timestamp,

            "uniqueidentifier" => SqlDbType.UniqueIdentifier,
            "xml" => SqlDbType.Xml,
            "sql_variant" => SqlDbType.Variant,

            _ => SqlDbType.NVarChar
        };

    public async Task<List<DvlSqlCreateTableExpression>> GetAllTablesAsync()
    {
        // 1. Get all columns per table (schema-qualified, since FK metadata needs schema too)
        var columnRows =
            await From("information_schema.tables as t")
                .Join("information_schema.columns as c", "t.table_name", "c.table_name")
                .Where(ConstantExpCol("t.table_schema") == ConstantExpCol("c.table_schema") &
                       ConstantExpCol("t.table_type") == "BASE TABLE" &
                       NotInExp("t.table_schema",  "\'pg_catalog\'", "\'information_schema\'"))
                .Select("t.table_schema", "t.table_name", "c.column_name", "c.data_type",
                    "c.character_maximum_length", "c.numeric_precision", "c.numeric_scale", "c.is_nullable", "c.column_default")
                .ToListAsync(row =>
                    (SchemaName: row["table_schema"].ToString()!,
                        TableName: row["table_name"].ToString()!,
                        Column: new DvlSqlCreateColumnExpression(row["column_name"].ToString()!)
                        {
                            Type = GetSqlDbType(row["data_type"].ToString()!),
                            Size = row.IsDBNull(row.GetOrdinal("character_maximum_length"))
                                ? null
                                : Convert.ToInt32(row["character_maximum_length"]), //todo it can be -1 on nvarchar(max). will it work?
                            Precision = row.IsDBNull(row.GetOrdinal("numeric_precision"))
                                ? null
                                : Convert.ToByte(row["numeric_precision"]),
                            Scale = row.IsDBNull(row.GetOrdinal("numeric_scale"))
                                ? null
                                : Convert.ToByte(row["numeric_scale"]),
                            IsNull = row.GetString(row.GetOrdinal("is_nullable")) == "YES",
                            DefaultExpression = row.IsDBNull(row.GetOrdinal("column_default"))
                                ? null
                                : new("___", row["column_default"].ToString()!, row["column_name"].ToString()!),
                        })
                );

        // 2. Build the table expressions
        var result = columnRows
            .GroupBy(c => (c.SchemaName, c.TableName))
            .Select(g => new DvlSqlCreateTableExpression(g.Key.TableName)
            {
                SchemaName = g.Key.SchemaName,
                ColumnExpressions = g.Select(c => c.Column).ToList()
            })
            .ToList();

        // Lookup used to attach PK/Unique/Index/FK metadata onto the already-created columns
        var columnsDict = columnRows
            .ToDictionary(c => (c.SchemaName, c.TableName, ColumnName: c.Column.Name), c => c.Column);

        // Table -> set of tables it depends on via FK, collected while walking the metadata below
        var dependencyMap = new Dictionary<string, HashSet<string>>();

        // 3. Get index / primary key / unique / foreign key metadata for every column
        (await From(AsExp("sys.tables", "t"))
                .Join(AsExp("sys.schemas", "s"), "s.schema_id", "t.schema_id")
                .Join(AsExp("sys.columns", "c"), "c.object_id", "t.object_id")

                // Index columns (key columns only, not INCLUDE columns)
                .LeftJoin(AsExp("sys.index_columns", "ic"),
                    ConstantExpCol("ic.object_id") == ConstantExpCol("t.object_id")
                    & ConstantExpCol("ic.column_id") == ConstantExpCol("c.column_id")
                    & ConstantExpCol("ic.is_included_column") == ConstantExpCol("0"))
                .LeftJoin(AsExp("sys.indexes", "i"),
                    ConstantExpCol("i.object_id") == ConstantExpCol("ic.object_id")
                    & ConstantExpCol("i.index_id") == ConstantExpCol("ic.index_id"))

                // Foreign keys where this column is the referencing (child) column
                .LeftJoin(AsExp("sys.foreign_key_columns", "fkc"),
                    ConstantExpCol("fkc.parent_object_id") == ConstantExpCol("t.object_id")
                    & ConstantExpCol("fkc.parent_column_id") == ConstantExpCol("c.column_id"))
                .LeftJoin(AsExp("sys.foreign_keys", "fk"), "fk.object_id", "fkc.constraint_object_id")
                .LeftJoin(AsExp("sys.tables", "ref_tbl"), "ref_tbl.object_id", "fkc.referenced_object_id")
                .LeftJoin(AsExp("sys.columns", "ref_col"),
                    ConstantExpCol("ref_col.object_id") == ConstantExpCol("fkc.referenced_object_id")
                    & ConstantExpCol("ref_col.column_id") == ConstantExpCol("fkc.referenced_column_id"))

                .Where(InExp("t.name", result.Select(r => ConstantExp(r.Name)).ToArray()) &
                       InExp("s.name", result.Select(r => ConstantExp(r.SchemaName)).ToArray()))
                .Select(
                    AsExp("t.name", "table_name"),
                    AsExp("s.name", "schema_name"),
                    AsExp("c.name", "column_name"),
                    AsExp("i.name", "index_name"),
                    AsExp("i.is_unique", "is_unique"),
                    AsExp("i.is_primary_key", "is_primary_key"),
                    AsExp("i.is_unique_constraint", "is_unique_constraint"),
                    AsExp("fk.name", "fk_name"),
                    AsExp("ref_tbl.name", "ref_table"),
                    AsExp("ref_col.name", "ref_column")
                )
                .ToListAsync(row =>
                    (
                        TableName: row["table_name"].ToString()!,
                        SchemaName: row["schema_name"].ToString()!,
                        ColumnName: row["column_name"].ToString()!,
                        IndexName: GetStr(row, "index_name"),
                        IsUnique: GetBool(row, "is_unique"),
                        IsPrimaryKey: GetBool(row, "is_primary_key"),
                        IsUniqueConstraint: GetBool(row, "is_unique_constraint"),
                        FkName: GetStr(row, "fk_name"),
                        RefTable: GetStr(row, "ref_table"),
                        RefColumn: GetStr(row, "ref_column")
                    )
                )
            )
            .GroupBy(c => (c.TableName, c.SchemaName, c.ColumnName))
            .ToList()
            .ForEach(g =>
            {
                if (!columnsDict.TryGetValue((g.Key.SchemaName, g.Key.TableName, g.Key.ColumnName), out var column))
                    return;

                // The join with indexes and FKs produces duplicate rows, so dedupe here
                var indexNames = string.Join(", ", g.Where(x => !string.IsNullOrEmpty(x.IndexName))
                    .Select(x => x.IndexName!).Distinct());
                var pkNames = string.Join(", ", g.Where(x => x.IsPrimaryKey && x.IndexName != null)
                    .Select(x => x.IndexName!).Distinct());
                var uqNames = string.Join(", ", g.Where(x => x.IsUniqueConstraint && x.IndexName != null)
                    .Select(x => x.IndexName!).Distinct());

                var isPrimaryKey = g.Any(x => x.IsPrimaryKey);
                var isUnique = g.Any(x => x.IsUnique);

                // Distinct on the whole triple keeps names, tables and columns aligned
                var fks = g.Where(x => x.FkName != null)
                    .Select(x => (Name: x.FkName!, Table: x.RefTable!, Column: x.RefColumn!))
                    .Distinct()
                    .ToList();

                if (isPrimaryKey)
                    column.PrimaryKeyExpression = new(pkNames, column.Name);

                if (indexNames.Length > 0)
                    column.IndexExpression = new(indexNames, g.Key.TableName, column.Name, isUnique);
                else if (isUnique)
                    column.UniqueExpression = new(uqNames, column.Name);

                if (fks.Count > 0)
                {
                    column.ForeignKeyExpression = new(
                        string.Join(", ", fks.Select(f => f.Name).Distinct()),
                        column.Name,
                        string.Join(", ", fks.Select(f => f.Table)),
                        string.Join(", ", fks.Select(f => f.Column)));

                    // Record dependency edges (table -> tables it references) for ordering step
                    if (!dependencyMap.TryGetValue(g.Key.TableName, out var deps))
                    {
                        deps = new HashSet<string>();
                        dependencyMap[g.Key.TableName] = deps;
                    }

                    foreach (var refTable in fks.Select(f => f.Table).Distinct())
                        if (refTable != g.Key.TableName)
                            deps.Add(refTable);
                }
            });

        // 4. Order tables: independent (no unresolved FK deps) tables first, dependent tables after
        return TopologicalSortTables(result, dependencyMap);
    }
    
    private static string? GetStr(IDataReader r, string col)
    {
        var i = r.GetOrdinal(col);
        return r.IsDBNull(i) ? null : r.GetString(i);
    }

    private static bool GetBool(IDataReader r, string col)
    {
        var i = r.GetOrdinal(col);
        return !r.IsDBNull(i) && r.GetBoolean(i);
    }

    private static List<DvlSqlCreateTableExpression> TopologicalSortTables(
        List<DvlSqlCreateTableExpression> tables,
        Dictionary<string, HashSet<string>> dependencyMap)
    {
        var tablesByName = tables.ToDictionary(t => t.Name);
        var ordered = new List<DvlSqlCreateTableExpression>();
        var visited = new HashSet<string>();
        var visiting = new HashSet<string>();

        void Visit(string tableName)
        {
            if (visited.Contains(tableName) || !tablesByName.ContainsKey(tableName))
                return;

            if (!visiting.Add(tableName))
                // Circular FK dependency detected; break the cycle instead of recursing forever.
                return;

            if (dependencyMap.TryGetValue(tableName, out var deps))
                foreach (var dependency in deps)
                    Visit(dependency);

            visiting.Remove(tableName);
            visited.Add(tableName);
            ordered.Add(tablesByName[tableName]);
        }

        foreach (var table in tables)
            Visit(table.Name);

        return ordered;
    }
    
    public async Task<DvlSqlCreateTableExpression?> GetTableAsync(string tableName)
    {
        var columns = await GetColumnsAsync(tableName);

        if (columns.Count == 0)
            return null;

        // Schema isn't strictly needed for a single-table lookup ordering-wise,
        // but we still fetch it to keep the table expression consistent with GetAllTablesAsync.
        string schemaName = await From("information_schema.tables as t")
            .Where(ConstantExpCol("t.table_name") == tableName &
                   ConstantExpCol("t.table_type") == "BASE TABLE" &
                   NotInExp("t.table_schema", "\'pg_catalog\'", "\'information_schema\'"))
            .Select("t.table_schema")
            .FirstAsync<string>();

        return new DvlSqlCreateTableExpression(tableName)
        {
            SchemaName = schemaName,
            ColumnExpressions = columns
        };
    }

    public async Task<List<DvlSqlCreateColumnExpression>> GetColumnsAsync(string tableName)
    {
        // 1. Get all columns for this table
        var columnRows =
            await From("information_schema.tables as t")
                .Join("information_schema.columns as c", "t.table_name", "c.table_name")
                .Where(ConstantExpCol("t.table_schema") == ConstantExpCol("c.table_schema") &
                       ConstantExpCol("t.table_type") == "BASE TABLE" &
                       ConstantExpCol("t.table_name") == tableName &
                       NotInExp("t.table_schema", "\'pg_catalog\'", "\'information_schema\'"))
                .Select("t.table_schema", "t.table_name", "c.column_name", "c.data_type",
                    "c.character_maximum_length", "c.numeric_precision", "c.numeric_scale", "c.is_nullable", "c.column_default")
                .ToListAsync(row =>
                    (SchemaName: row["table_schema"].ToString()!,
                        TableName: row["table_name"].ToString()!,
                        Column: new DvlSqlCreateColumnExpression(row["column_name"].ToString()!)
                        {
                            Type = GetSqlDbType(row["data_type"].ToString()!),
                            Size = row.IsDBNull(row.GetOrdinal("character_maximum_length"))
                                ? null
                                : Convert.ToInt32(row["character_maximum_length"]), //todo it can be -1 on nvarchar(max). will it work?
                            Precision = row.IsDBNull(row.GetOrdinal("numeric_precision"))
                                ? null
                                : Convert.ToByte(row["numeric_precision"]),
                            Scale = row.IsDBNull(row.GetOrdinal("numeric_scale"))
                                ? null
                                : Convert.ToByte(row["numeric_scale"]),
                            IsNull = row.GetString(row.GetOrdinal("is_nullable")) == "YES",
                            DefaultExpression = row.IsDBNull(row.GetOrdinal("column_default"))
                                ? null
                                : new("___", row["column_default"].ToString()!, row["column_name"].ToString()!),
                        })
                );

        if (columnRows.Count == 0)
            return new List<DvlSqlCreateColumnExpression>();

        var schemaName = columnRows[0].SchemaName;

        var columnsDict = columnRows
            .ToDictionary(c => c.Column.Name, c => c.Column);

        // 2. Get index / primary key / unique / foreign key metadata for this table's columns
        (await From(AsExp("sys.tables", "t"))
                .Join(AsExp("sys.schemas", "s"), "s.schema_id", "t.schema_id")
                .Join(AsExp("sys.columns", "c"), "c.object_id", "t.object_id")

                // Index columns (key columns only, not INCLUDE columns)
                .LeftJoin(AsExp("sys.index_columns", "ic"),
                    ConstantExpCol("ic.object_id") == ConstantExpCol("t.object_id")
                    & ConstantExpCol("ic.column_id") == ConstantExpCol("c.column_id")
                    & ConstantExpCol("ic.is_included_column") == ConstantExpCol("0"))
                .LeftJoin(AsExp("sys.indexes", "i"),
                    ConstantExpCol("i.object_id") == ConstantExpCol("ic.object_id")
                    & ConstantExpCol("i.index_id") == ConstantExpCol("ic.index_id"))

                // Foreign keys where this column is the referencing (child) column
                .LeftJoin(AsExp("sys.foreign_key_columns", "fkc"),
                    ConstantExpCol("fkc.parent_object_id") == ConstantExpCol("t.object_id")
                    & ConstantExpCol("fkc.parent_column_id") == ConstantExpCol("c.column_id"))
                .LeftJoin(AsExp("sys.foreign_keys", "fk"), "fk.object_id", "fkc.constraint_object_id")
                .LeftJoin(AsExp("sys.tables", "ref_tbl"), "ref_tbl.object_id", "fkc.referenced_object_id")
                .LeftJoin(AsExp("sys.columns", "ref_col"),
                    ConstantExpCol("ref_col.object_id") == ConstantExpCol("fkc.referenced_object_id")
                    & ConstantExpCol("ref_col.column_id") == ConstantExpCol("fkc.referenced_column_id"))
                .Where(ConstantExpCol("t.name") == tableName &
                       ConstantExpCol("s.name") == schemaName)
                .Select(
                    AsExp("t.name", "table_name"),
                    AsExp("s.name", "schema_name"),
                    AsExp("c.name", "column_name"),
                    AsExp("i.name", "index_name"),
                    AsExp("i.is_unique", "is_unique"),
                    AsExp("i.is_primary_key", "is_primary_key"),
                    AsExp("i.is_unique_constraint", "is_unique_constraint"),
                    AsExp("fk.name", "fk_name"),
                    AsExp("ref_tbl.name", "ref_table"),
                    AsExp("ref_col.name", "ref_column")
                )
                .ToListAsync(row =>
                    (
                        ColumnName: row["column_name"].ToString()!,
                        IndexName: GetStr(row, "index_name"),
                        IsUnique: GetBool(row, "is_unique"),
                        IsPrimaryKey: GetBool(row, "is_primary_key"),
                        IsUniqueConstraint: GetBool(row, "is_unique_constraint"),
                        FkName: GetStr(row, "fk_name"),
                        RefTable: GetStr(row, "ref_table"),
                        RefColumn: GetStr(row, "ref_column")
                    )
                )
            )
            .GroupBy(c => c.ColumnName)
            .ToList()
            .ForEach(g =>
            {
                if (!columnsDict.TryGetValue(g.Key, out var column))
                    return;

                static string Join(IEnumerable<string?> values) =>
                    string.Join(", ", values.Where(v => !string.IsNullOrEmpty(v)).Distinct());

                // Primary key: in SQL Server the PK constraint name is the index name
                var pkNames = Join(g.Where(x => x.IsPrimaryKey).Select(x => x.IndexName));
                if (pkNames.Length > 0)
                    column.PrimaryKeyExpression = new(pkNames, column.Name);

                // Named UNIQUE constraints (also backed by an index, hence the same name)
                var uqNames = Join(g.Where(x => x.IsUniqueConstraint).Select(x => x.IndexName));
                if (uqNames.Length > 0)
                    column.UniqueExpression = new(uqNames, column.Name);

                // Plain indexes: exclude the ones that only back a PK or UNIQUE constraint
                var plainIndexes = g.Where(x => !string.IsNullOrEmpty(x.IndexName)
                                                && !x.IsPrimaryKey
                                                && !x.IsUniqueConstraint)
                    .ToList();
                var indexNames = Join(plainIndexes.Select(x => x.IndexName));
                if (indexNames.Length > 0)
                    column.IndexExpression = new(indexNames, tableName, column.Name,
                        plainIndexes.Any(x => x.IsUnique));

                // Foreign keys
                var fkNames = Join(g.Select(x => x.FkName));
                if (fkNames.Length > 0)
                    column.ForeignKeyExpression = new(
                        fkNames,
                        column.Name,
                        Join(g.Select(x => x.RefTable)),
                        Join(g.Select(x => x.RefColumn)));
            });

        return columnRows.Select(c => c.Column).ToList();
    }

    public async Task<DvlSqlCreateColumnExpression?> GetColumnAsync(string tableName, string columnName)
    {
        var columns = await GetColumnsAsync(tableName);
        return columns.FirstOrDefault(c => c.Name == columnName);
    }
}