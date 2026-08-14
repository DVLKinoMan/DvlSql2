using System.Data;
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
        var result = (await From(AsExp("information_schema.tables", "t"))
                    .Join(AsExp("information_schema.columns", "c"), "t.table_name", "c.table_name")
                    .Where(ConstantExpCol("t.table_schema") == ConstantExpCol("c.table_schema") &
                          ConstantExpCol("t.table_type") == ConstantExpCol("BASE TABLE") &
                          NotInExp("t.table_schema", "pg_catalog", "information_schema"))
                    .Select("t.table_name", "c.column_name", "c.data_type", "c.character_maximum_length", "c.numeric_precision", "c.numeric_scale", "c.is_nullable",
                        "c.column_default")
                    .ToListAsync(row =>
                        (TableName: row["table_name"].ToString()!, 
                        Column: new DvlSqlCreateColumnExpression(row["column_name"].ToString()!)
                        {
                            Type = GetSqlDbType(row["data_type"].ToString()!),
                            Size = row.IsDBNull(row.GetOrdinal("character_maximum_length")) ? null : row.GetInt32(row.GetOrdinal("character_maximum_length")),
                            Precision = row.IsDBNull(row.GetOrdinal("numeric_precision")) ? null : row.GetByte(row.GetOrdinal("numeric_precision")),
                            Scale = row.IsDBNull(row.GetOrdinal("numeric_scale")) ? null : row.GetByte(row.GetOrdinal("numeric_scale")),
                            IsNull = row.GetString(row.GetOrdinal("is_nullable")) == "YES",
                            DefaultExpression = row.IsDBNull(row.GetOrdinal("column_default")) ? null : new("___", row["column_default"].ToString()!, row["column_name"].ToString()!),
                        })
                    )
                )
                .GroupBy(c => c.TableName)
                .Select(g => new DvlSqlCreateTableExpression(g.Key)
                {
                    ColumnExpressions = g.Select(c => c.Column).ToList()
                })
                .ToList();

        (await From(AsExp("pg_index", "ix"))
                .Join(AsExp("pg_class", "i"), "i.oid", "ix.indexrelid")
                .Join(AsExp("pg_class", "tb"), "tb.oid", "ix.indrelid")
                .Join(AsExp("pg_namespace", "n"), "n.oid", "tb.relnamespace")
                .Join(AsExp("pg_attribute", "a"), ConstantExpCol("a.attrelid") == ConstantExpCol("tb.oid")
                                                  & ConstantExpCol("a.attnum") == ConstantExpCol(AnyExp("ix.indkey")))
                // Primary key constraint (if this index backs a PK)
                .LeftJoin(AsExp("pg_constraint", "pk_con"), "pk_con.conrelid", "tb.oid")
                .Join(AsExp("pg_attribute", "a"), "a.attrelid", "tb.oid")
                .Join(AsExp("pg_attribute", "a"), "a.attrelid", "tb.oid")
                .Where(ConstantExpCol("a.attnum") == ConstantExpCol(AnyExp("ix.indkey")) &
                       ConstantExpCol("t.table_type") == "BASE TABLE" &
                       NotInExp("t.table_schema", "pg_catalog", "information_schema"))
                .Select("t.table_name", "c.column_name", "c.data_type", "c.character_maximum_length", "c.numeric_precision", "c.numeric_scale", "c.is_nullable",
                    "c.column_default")
                .ToListAsync(row =>
                    (TableName: row["table_name"].ToString()!, 
                        Column: new DvlSqlCreateColumnExpression(row["column_name"].ToString()!)
                        {
                            Type = GetSqlDbType(row["data_type"].ToString()!),
                            Size = row.IsDBNull(row.GetOrdinal("character_maximum_length")) ? null : row.GetInt32(row.GetOrdinal("character_maximum_length")),
                            Precision = row.IsDBNull(row.GetOrdinal("numeric_precision")) ? null : row.GetByte(row.GetOrdinal("numeric_precision")),
                            Scale = row.IsDBNull(row.GetOrdinal("numeric_scale")) ? null : row.GetByte(row.GetOrdinal("numeric_scale")),
                            IsNull = row.GetString(row.GetOrdinal("is_nullable")) == "YES",
                            DefaultExpression = row.IsDBNull(row.GetOrdinal("column_default")) ? null : new("___", row["column_default"].ToString()!, row["column_name"].ToString()!),
                        })
                )
            )
            .GroupBy(c => c.TableName)
            .Select(g => new DvlSqlCreateTableExpression(g.Key)
            {
                ColumnExpressions = g.Select(c => c.Column).ToList()
            })
            .ToList();

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