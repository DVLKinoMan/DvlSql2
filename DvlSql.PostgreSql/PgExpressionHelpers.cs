using System.Globalization;
using DvlSql.Expressions;

namespace DvlSql;

public static class PgExpressionHelpers
{
     #region 1. PostgreSQL-specific

    // [alias] -> "alias".
    // NOTE: quoted identifiers are case-sensitive in PostgreSQL. Keep aliases lowercase,
    // otherwise references like ix.col (unquoted, folded to lowercase) will not match "Ix".
    private static string WithAliasQuotes(string alias) =>
        alias.Length > 1 && alias[0] == '"' && alias[^1] == '"'
            ? alias
            : $"\"{alias.Replace("\"", "\"\"")}\"";

    // GETDATE() -> NOW()  (use LOCALTIMESTAMP for a value without time zone)
    public static string GetDateExp() => "NOW()";

    // EXTRACT returns numeric in PostgreSQL, so cast to integer to keep GetInt32 working
    public static string YearExp(string param) => $"CAST(EXTRACT(YEAR FROM {param}) AS integer)";
    public static string MonthExp(string param) => $"CAST(EXTRACT(MONTH FROM {param}) AS integer)";
    public static string DayExp(string param) => $"CAST(EXTRACT(DAY FROM {param}) AS integer)";

    // ISNULL(a, b) -> COALESCE(a, b)
    public static string IsNullExp(string param1, string param2) => $"COALESCE({param1}, {param2})";

    // CONVERT(type, x) -> CAST(x AS type)
    // Type names differ between dialects (nvarchar -> varchar, datetime -> timestamp, bit -> boolean ...)
    public static string ConvertExp(string type, string name) => $"CAST({name} AS {type})";

    // DATEDIFF(interval, start, end) emulation. Like SQL Server, it counts boundaries crossed,
    // not elapsed time.
    public static string DateDiffExp(string interval, string starting, string ending) =>
        interval.Trim().ToLowerInvariant() switch
        {
            "year" or "yy" or "yyyy" =>
                $"CAST(EXTRACT(YEAR FROM {ending}) - EXTRACT(YEAR FROM {starting}) AS integer)",
            "month" or "mm" or "m" =>
                $"CAST((EXTRACT(YEAR FROM {ending}) - EXTRACT(YEAR FROM {starting})) * 12 " +
                $"+ EXTRACT(MONTH FROM {ending}) - EXTRACT(MONTH FROM {starting}) AS integer)",
            "day" or "dd" or "d" =>
                $"(CAST({ending} AS date) - CAST({starting} AS date))",
            "hour" or "hh" => EpochDiff("hour", 3600, starting, ending),
            "minute" or "mi" or "n" => EpochDiff("minute", 60, starting, ending),
            "second" or "ss" or "s" => EpochDiff("second", 1, starting, ending),
            _ => throw new NotSupportedException($"DATEDIFF interval '{interval}' is not supported for PostgreSQL")
        };

    private static string EpochDiff(string unit, int seconds, string starting, string ending) =>
        $"CAST(EXTRACT(EPOCH FROM (date_trunc('{unit}', {ending}) - date_trunc('{unit}', {starting}))) / {seconds} AS integer)";

    // Dates are written in ISO format (independent of the server DateStyle) and quotes are escaped
    public static string CoalesceExp<T>(string param, T change) =>
        typeof(T) == typeof(string) || typeof(T) == typeof(Guid) || typeof(T) == typeof(Guid?)
        || typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?)
            ? $"COALESCE({param}, '{(change is DateTime dt ? dt.ToString("yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture) : change!.ToString()!.Replace("'", "''"))}')"
            : $"COALESCE({param}, {change})";

    // Already PostgreSQL-only in the original, kept here (ANY upper-cased, function names are case-insensitive)
    public static string BoolOrExp(string field) => $"BOOL_OR({field})";

    public static string StringAggExp(string field, string seperator) =>
        $"STRING_AGG({field}, '{seperator.Replace("'", "''")}')";

    public static string AnyExp(string param) => $"ANY({param})";

    #endregion

    #region 2. New PostgreSQL-only helpers

    public static string BoolAndExp(string field) => $"BOOL_AND({field})";

    public static string ArrayAggExp(string field) => $"ARRAY_AGG({field})";

    public static string CastExp(string name, string type) => $"CAST({name} AS {type})";

    // x::type shorthand, e.g. ColonCastExp("@p1", "\"char\"") -> @p1::"char"
    public static string ColonCastExp(string name, string type) => $"{name}::{type}";

    public static string LocalTimestampExp() => "LOCALTIMESTAMP";

    public static string DateTruncExp(string unit, string field) => $"date_trunc('{unit}', {field})";

    // 1-based, like fk_con.conkey in pg_constraint
    public static string ArrayPositionExp(string array, string element) => $"array_position({array}, {element})";

    public static string ArrayItemExp(string array, string index) => $"{array}[{index}]";

    #endregion
}