using System.Data;
using Npgsql;
using static System.Exts.Extensions;

namespace DvlSql.PostgreSql;

internal static class Exts
{
    internal static IEnumerable<NpgsqlParameter> ToSqlParameters(this IEnumerable<DvlSqlParameter> parameters) =>
        parameters.Select(param => param.ToSqlParameter());

    internal static NpgsqlParameter ToSqlParameter(this DvlSqlParameter parameter)
    {
        var isOuput = parameter is DvlSqlOutputParameter;
        var param = new NpgsqlParameter(parameter.Name.GetStringAfter("."), parameter.DvlSqlType.SqlDbType)
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
    
    //todo needs check for posgresql. It is copyed from mssql
    public static string ToSqlString(
        this SqlDbType type,
        int? size = null,
        byte? precision = null,
        byte? scale = null)
        => type switch
        {
            SqlDbType.BigInt => "BIGINT",
            SqlDbType.Binary => "BYTEA",
            SqlDbType.Bit => "BOOLEAN",
            SqlDbType.Char => $"CHAR({size ?? 1})",
            SqlDbType.Date => "DATE",
            SqlDbType.DateTime => "TIMESTAMP",
            SqlDbType.DateTime2 => precision.HasValue
                ? $"TIMESTAMP({precision})"
                : "TIMESTAMP",
            SqlDbType.DateTimeOffset => precision.HasValue
                ? $"TIMESTAMPTZ({precision})"
                : "TIMESTAMPTZ",
            SqlDbType.Decimal => $"NUMERIC({precision ?? 18},{scale ?? 0})",
            SqlDbType.Float => "DOUBLE PRECISION",
            SqlDbType.Image => "BYTEA",
            SqlDbType.Int => "INTEGER",
            SqlDbType.Money => "MONEY",
            SqlDbType.NChar => $"CHAR({size ?? 1})",
            SqlDbType.NText => "TEXT",
            SqlDbType.NVarChar => size == -1
                ? "TEXT"
                : $"VARCHAR({size ?? 1})",
            SqlDbType.Real => "REAL",
            SqlDbType.SmallDateTime => "TIMESTAMP",
            SqlDbType.SmallInt => "SMALLINT",
            SqlDbType.SmallMoney => "NUMERIC(10,4)",
            SqlDbType.Text => "TEXT",
            SqlDbType.Time => precision.HasValue
                ? $"TIME({precision})"
                : "TIME",
            SqlDbType.Timestamp => "BYTEA",
            SqlDbType.TinyInt => "SMALLINT",
            SqlDbType.UniqueIdentifier => "UUID",
            SqlDbType.VarBinary => "BYTEA",
            SqlDbType.VarChar => size == -1
                ? "TEXT"
                : $"VARCHAR({size ?? 1})",
            SqlDbType.Variant => "TEXT",
            SqlDbType.Xml => "XML",
            _ => throw new NotSupportedException($"Unsupported SqlDbType: {type}")
        };
}