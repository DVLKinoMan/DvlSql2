using System.Data;
using Microsoft.Data.SqlClient;
using static System.Exts.Extensions;

namespace DvlSql.SqlServer;

internal static class Exts
{
    internal static IEnumerable<SqlParameter> ToSqlParameters(this IEnumerable<DvlSqlParameter> parameters) =>
        parameters.Select(param => param.ToSqlParameter());

    internal static SqlParameter ToSqlParameter(this DvlSqlParameter parameter)
    {
        var isOuput = parameter is DvlSqlOutputParameter;
        var param = new SqlParameter(parameter.Name.GetStringAfter("."), parameter.DvlSqlType.SqlDbType)
        {
            Direction = isOuput ? ParameterDirection.Output : ParameterDirection.Input
        };

        if (isOuput)
            param.Value = DBNull.Value;
        else if (parameter.DvlSqlType.GetType().GetGenericTypeDefinition() == typeof(DvlSqlType<>))
        {
            var prop = parameter.DvlSqlType.GetType().GetProperty("Value") ?? throw new MissingMemberException("Value");
            param.Value = prop.GetValue(parameter.DvlSqlType) ?? DBNull.Value;
        }

        if (parameter.DvlSqlType.Size != null)
            param.Size = parameter.DvlSqlType.Size.Value;

        if (parameter.DvlSqlType.Precision != null)
            param.Precision = parameter.DvlSqlType.Precision.Value;

        if (parameter.DvlSqlType.Scale != null)
            param.Scale = parameter.DvlSqlType.Scale.Value;

        return param;
    }

    public static string ToSqlString(
        this SqlDbType type,
        int? size = null,
        byte? precision = null,
        byte? scale = null)
        => type switch
        {
            SqlDbType.BigInt => "BIGINT",
            SqlDbType.Binary => $"BINARY({size ?? 1})",
            SqlDbType.Bit => "BIT",
            SqlDbType.Char => $"CHAR({size ?? 1})",
            SqlDbType.Date => "DATE",
            SqlDbType.DateTime => "DATETIME",
            SqlDbType.DateTime2 => precision.HasValue
                ? $"DATETIME2({precision})"
                : "DATETIME2",
            SqlDbType.DateTimeOffset => precision.HasValue
                ? $"DATETIMEOFFSET({precision})"
                : "DATETIMEOFFSET",
            SqlDbType.Decimal => $"DECIMAL({precision ?? 18},{scale ?? 0})",
            SqlDbType.Float => "FLOAT",
            SqlDbType.Image => "IMAGE",
            SqlDbType.Int => "INT",
            SqlDbType.Money => "MONEY",
            SqlDbType.NChar => $"NCHAR({size ?? 1})",
            SqlDbType.NText => "NTEXT",
            SqlDbType.NVarChar => size == -1
                ? "NVARCHAR(MAX)"
                : $"NVARCHAR({size ?? 1})",
            SqlDbType.Real => "REAL",
            SqlDbType.SmallDateTime => "SMALLDATETIME",
            SqlDbType.SmallInt => "SMALLINT",
            SqlDbType.SmallMoney => "SMALLMONEY",
            SqlDbType.Text => "TEXT",
            SqlDbType.Time => precision.HasValue
                ? $"TIME({precision})"
                : "TIME",
            SqlDbType.Timestamp => "ROWVERSION",
            SqlDbType.TinyInt => "TINYINT",
            SqlDbType.UniqueIdentifier => "UNIQUEIDENTIFIER",
            SqlDbType.VarBinary => size == -1
                ? "VARBINARY(MAX)"
                : $"VARBINARY({size ?? 1})",
            SqlDbType.VarChar => size == -1
                ? "VARCHAR(MAX)"
                : $"VARCHAR({size ?? 1})",
            SqlDbType.Variant => "SQL_VARIANT",
            SqlDbType.Xml => "XML",
            _ => throw new NotSupportedException($"Unsupported SqlDbType: {type}")
        };
}