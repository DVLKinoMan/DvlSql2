using System.Data;
using DvlSql.Expressions;
using static DvlSql.ExpressionHelpers;

namespace DvlSql.SqlServer;

partial class DvlSqlMs
{
   private static SqlDbType GetSqlDbType(string dataType) =>
        dataType.Trim().ToLowerInvariant() switch
        {
            // Numeric types
            "smallint" => SqlDbType.SmallInt,
            "integer" => SqlDbType.Int,
            "bigint" => SqlDbType.BigInt,
            "decimal" => SqlDbType.Decimal,
            "numeric" => SqlDbType.Decimal,
            "real" => SqlDbType.Real,
            "double precision" => SqlDbType.Float,
            "smallserial" => SqlDbType.SmallInt,
            "serial" => SqlDbType.Int,
            "bigserial" => SqlDbType.BigInt,
            "money" => SqlDbType.Money,

            // Character types
            "character varying" => SqlDbType.NVarChar,
            "varchar" => SqlDbType.NVarChar,
            "character" => SqlDbType.NChar,
            "char" => SqlDbType.NChar,
            "text" => SqlDbType.NVarChar, // map to NVarChar(MAX) at column-build time
            "citext" => SqlDbType.NVarChar,

            // Boolean
            "boolean" => SqlDbType.Bit,

            // Date/time types
            "date" => SqlDbType.Date,
            "time without time zone" => SqlDbType.Time,
            "time with time zone" => SqlDbType.Time,
            "timestamp without time zone" => SqlDbType.DateTime2,
            "timestamp with time zone" => SqlDbType.DateTimeOffset,
            "interval" => SqlDbType.NVarChar, // no direct SQL Server equivalent

            // Binary
            "bytea" => SqlDbType.VarBinary,

            // UUID
            "uuid" => SqlDbType.UniqueIdentifier,

            // JSON types
            "json" => SqlDbType.NVarChar,
            "jsonb" => SqlDbType.NVarChar,

            // XML
            "xml" => SqlDbType.Xml,

            // Network address types (no native SQL Server equivalent)
            "inet" => SqlDbType.NVarChar,
            "cidr" => SqlDbType.NVarChar,
            "macaddr" => SqlDbType.NVarChar,

            // Arrays and other Postgres-specific types (no direct equivalent)
            "array" => SqlDbType.NVarChar,
            "hstore" => SqlDbType.NVarChar,
            "point" => SqlDbType.NVarChar,
            "geometry" => SqlDbType.NVarChar,

            // Fallback for anything unmapped
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
                    "c.character_maximum_length", "c.numeric_precision", "c.numeric_scale", "c.is_nullable")
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
                            // DefaultExpression = row.IsDBNull(row.GetOrdinal("column_default"))
                            //     ? null
                            //     : new("___", row["column_default"].ToString()!, row["column_name"].ToString()!),
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
                    "c.character_maximum_length", "c.numeric_precision", "c.numeric_scale", "c.is_nullable")
                .ToListAsync(row =>
                    (SchemaName: row["table_schema"].ToString()!,
                        TableName: row["table_name"].ToString()!,
                        Column: new DvlSqlCreateColumnExpression(row["column_name"].ToString()!)
                        {
                            Type = GetSqlDbType(row["data_type"].ToString()!),
                            Size = row.IsDBNull(row.GetOrdinal("character_maximum_length"))
                                ? null
                                : row.GetInt32(row.GetOrdinal("character_maximum_length")),
                            Precision = row.IsDBNull(row.GetOrdinal("numeric_precision"))
                                ? null
                                : row.GetByte(row.GetOrdinal("numeric_precision")),
                            Scale = row.IsDBNull(row.GetOrdinal("numeric_scale"))
                                ? null
                                : row.GetByte(row.GetOrdinal("numeric_scale")),
                            IsNull = row.GetString(row.GetOrdinal("is_nullable")) == "YES",
                        })
                );

        if (columnRows.Count == 0)
            return new List<DvlSqlCreateColumnExpression>();

        var schemaName = columnRows[0].SchemaName;

        var columnsDict = columnRows
            .ToDictionary(c => c.Column.Name, c => c.Column);

        // 2. Get index / primary key / unique / foreign key metadata for this table's columns
        (await From(AsExp("pg_index", "ix"))
                .Join(AsExp("pg_class", "i"), "i.oid", "ix.indexrelid")
                .Join(AsExp("pg_class", "tb"), "tb.oid", "ix.indrelid")
                .Join(AsExp("pg_namespace", "n"), "n.oid", "tb.relnamespace")
                .Join(AsExp("pg_attribute", "a"), ConstantExpCol("a.attrelid") == ConstantExpCol("tb.oid")
                                                  & ConstantExpCol("a.attnum") == ConstantExpCol(AnyExp("ix.indkey")))
                .LeftJoin(AsExp("pg_constraint", "pk_con"),
                    ConstantExpCol("pk_con.conrelid") == ConstantExpCol("tb.oid")
                    & ConstantExpCol("pk_con.contype") == "p"
                    & ConstantExpCol("a.attnum") == ConstantExpCol(AnyExp("pk_con.conkey")))
                .LeftJoin(AsExp("pg_constraint", "uq_con"),
                    ConstantExpCol("uq_con.conrelid") == ConstantExpCol("tb.oid")
                    & ConstantExpCol("uq_con.contype") == "u"
                    & ConstantExpCol("a.attnum") == ConstantExpCol(AnyExp("uq_con.conkey")))
                .LeftJoin(AsExp("pg_constraint", "fk_con"),
                    ConstantExpCol("fk_con.conrelid") == ConstantExpCol("tb.oid")
                    & ConstantExpCol("fk_con.contype") == "f"
                    & ConstantExpCol("a.attnum") == ConstantExpCol(AnyExp("fk_con.conkey")))
                .LeftJoin(AsExp("fk_con", "ref_tbl"), "ref_tbl.oid", "fk_con.confrelid")
                .LeftJoin(AsExp("pg_attribute", "ref_col"),
                    ConstantExpCol("ref_col.attrelid") == ConstantExpCol("fk_con.confrelid")
                    & ConstantExpCol("ref_col.attnum") ==
                    ConstantExpCol("fk_con.confkey[array_position(fk_con.conkey, a.attnum)]"))
                .Where(ConstantExpCol("tb.relname") == tableName &
                       ConstantExpCol("n.nspname") == schemaName)
                .Select(
                    AsExp("tb.relname", "table_name"),
                    AsExp("n.nspname", "schema_name"),
                    AsExp("a.attname", "column_name"),
                    AsExp(StringAggExp(DistinctExp("i.relname"), ", "), "index_names"),
                    AsExp(BoolOrExp(DistinctExp("ix.indisunique")), "is_unique"),
                    AsExp(BoolOrExp(DistinctExp("ix.indisprimary")), "is_primary_key"),
                    AsExp(StringAggExp(DistinctExp("pk_con.conname"), ", "), "primary_key_constraint_names"),
                    AsExp(StringAggExp(DistinctExp("uq_con.conname"), ", "), "unique_constraint_names"),
                    AsExp(StringAggExp(DistinctExp("fk_con.conname"), ", "), "foreign_key_constraint_names"),
                    AsExp(StringAggExp(DistinctExp("ref_tbl.relname"), ", "), "foreign_key_reference_tables"),
                    AsExp(StringAggExp(DistinctExp("ref_col.attname"), ", "), "foreign_key_reference_columns")
                )
                .ToListAsync(row =>
                    (
                        ColumnName: row["column_name"].ToString()!,
                        IndexNames: row["index_names"].ToString()!,
                        IsUnique: row.GetBoolean(row.GetOrdinal("is_unique")),
                        IsPrimaryKey: row.GetBoolean(row.GetOrdinal("is_primary_key")),
                        PrimaryKeyConstraintNames: row["primary_key_constraint_names"].ToString()!,
                        UniqueConstraintNames: row["unique_constraint_names"].ToString()!,
                        ForeignKeyConstraintNames: row["foreign_key_constraint_names"].ToString()!,
                        ForeignKeyReferenceTables: row["foreign_key_reference_tables"].ToString()!,
                        ForeignKeyReferenceColumns: row["foreign_key_reference_columns"].ToString()!
                    )
                )
            )
            .GroupBy(c => c.ColumnName)
            .ToList()
            .ForEach(g =>
            {
                var value = g.First();

                if (!columnsDict.TryGetValue(g.Key, out var column))
                    return;

                if (value.IsPrimaryKey)
                    column.PrimaryKeyExpression = new(value.PrimaryKeyConstraintNames, column.Name);

                if (value.IndexNames is { Length: > 0 })
                    column.IndexExpression = new(value.IndexNames, tableName, column.Name, value.IsUnique);
                else if (value.IsUnique)
                    column.UniqueExpression = new(value.UniqueConstraintNames, column.Name);

                if (value.ForeignKeyConstraintNames is { Length: > 0 })
                    column.ForeignKeyExpression = new(value.ForeignKeyConstraintNames, column.Name,
                        value.ForeignKeyReferenceTables, value.ForeignKeyReferenceColumns);
            });

        return columnRows.Select(c => c.Column).ToList();
    }

    public async Task<DvlSqlCreateColumnExpression?> GetColumnAsync(string tableName, string columnName)
    {
        var columns = await GetColumnsAsync(tableName);
        return columns.FirstOrDefault(c => c.Name == columnName);
    }
}