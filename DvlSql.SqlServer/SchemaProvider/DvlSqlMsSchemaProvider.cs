using System.Data;
using DvlSql.Expressions;
using static DvlSql.ExpressionHelpers;

namespace DvlSql.SqlServer;

partial class DvlSqlMs
{
    private static SqlDbType GetSqlDbType(string dataType) =>
        dataType.Trim().ToLowerInvariant() switch
        {
            // Exact numerics
            "bit"                => SqlDbType.Bit,
            "tinyint"            => SqlDbType.TinyInt,
            "smallint"           => SqlDbType.SmallInt,
            "int"                => SqlDbType.Int,
            "bigint"             => SqlDbType.BigInt,
            "decimal"            => SqlDbType.Decimal,
            "numeric"            => SqlDbType.Decimal,
            "smallmoney"         => SqlDbType.SmallMoney,
            "money"              => SqlDbType.Money,

            // Approximate numerics
            "real"               => SqlDbType.Real,
            "float"              => SqlDbType.Float,

            // Date/time
            "date"               => SqlDbType.Date,
            "time"               => SqlDbType.Time,
            "datetime"           => SqlDbType.DateTime,
            "datetime2"          => SqlDbType.DateTime2,
            "smalldatetime"      => SqlDbType.SmallDateTime,
            "datetimeoffset"     => SqlDbType.DateTimeOffset,

            // Character strings
            "char"               => SqlDbType.Char,
            "varchar"            => SqlDbType.VarChar,
            "text"               => SqlDbType.Text,

            // Unicode character strings
            "nchar"              => SqlDbType.NChar,
            "nvarchar"           => SqlDbType.NVarChar,
            "ntext"              => SqlDbType.NText,

            // Binary strings
            "binary"             => SqlDbType.Binary,
            "varbinary"          => SqlDbType.VarBinary,
            "image"              => SqlDbType.Image,

            // Other types
            "uniqueidentifier"   => SqlDbType.UniqueIdentifier,
            "xml"                => SqlDbType.Xml,
            "sql_variant"        => SqlDbType.Variant,
            "timestamp"          => SqlDbType.Timestamp, // "rowversion" alias
            "rowversion"         => SqlDbType.Timestamp,
            "geography"          => SqlDbType.Udt,
            "geometry"           => SqlDbType.Udt,
            "hierarchyid"        => SqlDbType.Udt,
            "structured"         => SqlDbType.Structured, // table-valued params

            // Fallback for anything unmapped
            _                    => SqlDbType.NVarChar
        };

    public async Task<List<DvlSqlCreateTableExpression>> GetAllTablesAsync()
    {
        var columns =
            await From("information_schema.tables as t")
                .Join("information_schema.columns as c", "t.table_name", "c.table_name")
                .Where(ConstantExpCol("t.table_schema") == "c.table_schema" &
                       ConstantExpCol("t.table_type") == "BASE TABLE" &
                       NotInExp("t.table_schema", "pg_catalog", "information_schema"))
                .Select("t.table_name", "c.column_name", "c.data_type", "c.character_maximum_length", "c.numeric_precision", "c.numeric_scale", "c.is_nullable")
                .ToListAsync(row =>
                    (TableName: row["table_name"].ToString()!, 
                        Column: new DvlSqlCreateColumnExpression(row["column_name"].ToString()!)
                        {
                            Type = GetSqlDbType(row["data_type"].ToString()!),
                            Size = row.IsDBNull(row.GetOrdinal("character_maximum_length")) ? null : row.GetInt32(row.GetOrdinal("character_maximum_length")),
                            Precision = row.IsDBNull(row.GetOrdinal("numeric_precision")) ? null : row.GetByte(row.GetOrdinal("numeric_precision")),
                            Scale = row.IsDBNull(row.GetOrdinal("numeric_scale")) ? null : row.GetByte(row.GetOrdinal("numeric_scale")),
                            IsNull = row.GetString(row.GetOrdinal("is_nullable")) == "YES",
                            DefaultExpression = 
                        })
                );

        return columns
            .GroupBy(c => c.TableName)
            .Select(g => new DvlSqlCreateTableExpression(g.Key)
            {
                ColumnExpressions = g.Select(c => c.Column).ToList()
            })
            .ToList();
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