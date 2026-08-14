using System.Data;
using System.Data.Common;
using System.Runtime.ExceptionServices;
using DvlSql.Expressions;
using static DvlSql.ExpressionHelpers;

namespace DvlSql.PostgreSql;

partial class DvlPostgreSql
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
        var columnsDict = new Dictionary<(string SchemaName, string TableName, string ColumnName), DvlSqlCreateColumnExpression>();
        var result = (await From(AsExp("information_schema.tables", "t"))
                .Join(AsExp("information_schema.columns", "c"), "t.table_name", "c.table_name")
                .Where(ConstantExpCol("t.table_schema") == ConstantExpCol("c.table_schema") &
                       ConstantExpCol("t.table_type") == ConstantExpCol("BASE TABLE") &
                       NotInExp("t.table_schema", "pg_catalog", "information_schema"))
                .Select("t.table_schema", "t.table_name", "c.column_name", "c.data_type", "c.character_maximum_length", "c.numeric_precision",
                    "c.numeric_scale", "c.is_nullable",
                    "c.column_default")
                .ToListAsync(row =>
                    (
                        SchemaName: row["table_schema"].ToString()!,
                        TableName: row["table_name"].ToString()!,
                        Column: new DvlSqlCreateColumnExpression(row["column_name"].ToString()!)
                        {
                            Type = GetSqlDbType(row["data_type"].ToString()!),
                            Size = row.IsDBNull(row.GetOrdinal("character_maximum_length")) ? null : row.GetInt32(row.GetOrdinal("character_maximum_length")),
                            Precision = row.IsDBNull(row.GetOrdinal("numeric_precision")) ? null : row.GetByte(row.GetOrdinal("numeric_precision")),
                            Scale = row.IsDBNull(row.GetOrdinal("numeric_scale")) ? null : row.GetByte(row.GetOrdinal("numeric_scale")),
                            IsNull = row.GetString(row.GetOrdinal("is_nullable")) == "YES",
                            DefaultExpression = row.IsDBNull(row.GetOrdinal("column_default"))
                                ? null
                                : new("___", row["column_default"].ToString()!, row["column_name"].ToString()!),
                        })
                )
            )
            .GroupBy(c => (c.TableName, c.SchemaName))
            .Select(g =>
            {
                foreach (var c in g)
                    columnsDict[(c.SchemaName, c.TableName, c.Column.Name)] = c.Column;

                return new DvlSqlCreateTableExpression(g.Key.TableName, schemaName: g.Key.SchemaName)
                {
                    ColumnExpressions = g.Select(c => c.Column).ToList()
                };
            })
            .ToList();

        (await From(AsExp("pg_index", "ix"))
                .Join(AsExp("pg_class", "i"), "i.oid", "ix.indexrelid")
                .Join(AsExp("pg_class", "tb"), "tb.oid", "ix.indrelid")
                .Join(AsExp("pg_namespace", "n"), "n.oid", "tb.relnamespace")
                .Join(AsExp("pg_attribute", "a"), ConstantExpCol("a.attrelid") == ConstantExpCol("tb.oid")
                                                  & ConstantExpCol("a.attnum") == ConstantExpCol(AnyExp("ix.indkey")))
                // Primary key constraint (if this index backs a PK)
                .LeftJoin(AsExp("pg_constraint", "pk_con"), ConstantExpCol("pk_con.conrelid") == ConstantExpCol("tb.oid")
                                                            & ConstantExpCol("pk_con.contype") == "p"
                                                            & ConstantExpCol("a.attnum") == ConstantExpCol(AnyExp("pk_con.conkey")))
                // Unique constraint (if this index backs a named UNIQUE constraint, not just a bare unique index)
                .LeftJoin(AsExp("pg_constraint", "uq_con"), ConstantExpCol("uq_con.conrelid") == ConstantExpCol("tb.oid")
                                                            & ConstantExpCol("uq_con.contype") == "u"
                                                            & ConstantExpCol("a.attnum") == ConstantExpCol(AnyExp("uq_con.conkey")))
                // Foreign key constraint where this column is the referencing (child) column
                .LeftJoin(AsExp("pg_constraint", "fk_con"), ConstantExpCol("fk_con.conrelid") == ConstantExpCol("tb.oid")
                                                            & ConstantExpCol("fk_con.contype") == "f"
                                                            & ConstantExpCol("a.attnum") == ConstantExpCol(AnyExp("fk_con.conkey")))
                .LeftJoin(AsExp("fk_con", "ref_tbl"), "ref_tbl.oid", "fk_con.confrelid")
                .LeftJoin(AsExp("pg_attribute", "ref_col"), ConstantExpCol("ref_col.attrelid") == ConstantExpCol("fk_con.confrelid")
                                                            & ConstantExpCol("ref_col.attnum") ==
                                                            ConstantExpCol("fk_con.confkey[array_position(fk_con.conkey, a.attnum)]"))
                .Where(InExp("tb.relname", result.Select(r => ConstantExp(r.Name)).ToArray()) &
                       InExp("n.nspname", result.Select(r => ConstantExp(r.SchemaName)).ToArray()))
                .Select(
                    AsExp("tb.relname", "table_name"),
                    AsExp("tn.nspname", "schema_name"),
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
                        TableName: row["table_name"].ToString()!,
                        SchemaName: row["schema_name"].ToString()!,
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
            .GroupBy(c => (c.TableName, c.SchemaName, c.ColumnName))
            .ToList()
            .ForEach(g =>
            {
                var value = g.First();
                var column = columnsDict[(g.Key.SchemaName, g.Key.TableName, g.Key.ColumnName)];

                if (value.IsPrimaryKey)
                    column.PrimaryKeyExpression = new(value.PrimaryKeyConstraintNames, column.Name);

                if (value.IndexNames is { Length: > 0 })
                    column.IndexExpression = new(value.IndexNames, g.Key.TableName, column.Name, value.IsUnique);
                else if (value.IsUnique)
                    column.UniqueExpression = new(value.UniqueConstraintNames, column.Name);

                if (value.ForeignKeyConstraintNames is { Length: > 0 })
                    column.ForeignKeyExpression = new(value.ForeignKeyConstraintNames, column.Name, value.ForeignKeyReferenceTables,
                        value.ForeignKeyReferenceColumns);
            });

        return result;
    }


    public Task<DvlSqlCreateTableExpression?> GetTableAsync(string tableName)
    {
        throw new NotImplementedException();
    }

    public Task<List<DvlSqlCreateColumnExpression>> GetColumnsAsync(string tableName)
    {
        throw new NotImplementedException();
    }

    public Task<DvlSqlCreateColumnExpression?> GetColumnAsync(string tableName, string columnName)
    {
        throw new NotImplementedException();
    }
}