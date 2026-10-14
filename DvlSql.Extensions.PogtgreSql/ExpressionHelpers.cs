using System.Globalization;
using System.Runtime.CompilerServices;
using DvlSql.Expressions;
using static System.Exts.Extensions;

// ReSharper disable once CheckNamespace
namespace DvlSql;

public static class PgExpressionHelpers
{
    #region 1. PostgreSQL-specific (replacing SQL Server versions)

    // [alias] -> "alias".
    // NOTE: quoted identifiers are case-sensitive in PostgreSQL. Keep aliases lowercase,
    // otherwise references like ix.col (unquoted, folded to lowercase) will not match "Ix".
    private static string WithAliasQuotes(string alias) =>
        alias.Length > 1 && alias[0] == '"' && alias[^1] == '"'
            ? alias
            : $"\"{alias.Replace("\"", "\"\"")}\"";

    public static string AsExp(string field, string @as) =>
        @as != null ? $"{field} AS {WithAliasQuotes(@as)}" : field;

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

    // PostgreSQL has no NOLOCK hint (MVCC: readers do not block writers), so the flag is ignored
    public static DvlSqlFromWithTableExpression FromExp(string tableName, string? @as = null, bool withNoLock = false)
    {
        var from = new DvlSqlFromWithTableExpression(tableName, false);
        if (@as is not null)
            from.As = AsExp(@as);
        return from;
    }

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

    #region 3. Shared (identical for SQL Server and PostgreSQL)

    public static string AvgExp(string param) => $"AVG({param})";
    public static string CountExp(string param = "*") => $"COUNT({param})";
    public static string MaxExp(string param) => $"MAX({param})";
    public static string MinExp(string param) => $"MIN({param})";
    public static string SumExp(string param) => $"SUM({param})";
    public static string DistinctExp(string param) => $"DISTINCT({param})";

    public static DvlSqlAsExpression AsExp(string @as, IEnumerable<string>? @params = null, bool useAs = true) =>
        new(@as, @params, useAs);

    public static DvlSqlWhereExpression WhereExp(DvlSqlBinaryExpression innerExpression, bool isRoot = false) =>
        (new DvlSqlWhereExpression(innerExpression)
            .WithRoot(isRoot)
            .WithParameters(innerExpression.Parameters) as DvlSqlWhereExpression)!;

    public static DvlSqlAndExpression AndExp(params DvlSqlBinaryExpression[] innerExpressions) =>
        new(innerExpressions);

    public static DvlSqlAndExpression AndExp(IEnumerable<DvlSqlBinaryExpression> innerExpressions) =>
        new(innerExpressions);

    public static DvlSqlComparisonExpression<T> ComparisonExp<T>(DvlSqlComparableExpression<T> leftExp,
        SqlComparisonOperator op,
        DvlSqlComparableExpression<T> rightExp)
        => new(leftExp, op, rightExp);

    public static DvlSqlConstantExpression<TValue> ConstantExp<TValue>(TValue value, string? name = null) =>
        new(value, name);

    public static DvlSqlConstantColumnExpression ConstantExpCol(string columnName) => new(columnName);

    public static DvlSqlMemberExpressionString MemberExpString(string memberName) => new(memberName);

    public static DvlSqlMemberExpressionGuid MemberExpGuid(string memberName) => new(memberName);

    public static DvlSqlMemberExpressionInt MemberExpInt(string memberName) => new(memberName);

    public static DvlSqlMemberExpressionLong MemberExpLong(string memberName) => new(memberName);

    public static DvlSqlMemberExpressionDouble MemberExpDouble(string memberName) => new(memberName);

    public static DvlSqlMemberExpressionDecimal MemberExpDecimal(string memberName) => new(memberName);

    public static DvlSqlMemberExpressionDateTime MemberExpDateTime(string memberName) => new(memberName);

    public static DvlSqlMemberExpressionBool MemberExpBool(string memberName) => new(memberName);

    public static DvlSqlMemberExpressionByte MemberExpByte(string memberName) => new(memberName);

    public static DvlSqlMemberExpression<TValue> MemberExp<TValue>(string memberName) =>
        typeof(TValue) switch
        {
            _ when typeof(TValue) == typeof(string) => (MemberExpString(memberName) as DvlSqlMemberExpression<TValue>)!,
            _ when typeof(TValue) == typeof(int) => (MemberExpInt(memberName) as DvlSqlMemberExpression<TValue>)!,
            _ when typeof(TValue) == typeof(Guid) => (MemberExpGuid(memberName) as DvlSqlMemberExpression<TValue>)!,
            _ when typeof(TValue) == typeof(double) => (MemberExpDouble(memberName) as DvlSqlMemberExpression<TValue>)!,
            _ when typeof(TValue) == typeof(long) => (MemberExpLong(memberName) as DvlSqlMemberExpression<TValue>)!,
            _ when typeof(TValue) == typeof(bool) => (MemberExpBool(memberName) as DvlSqlMemberExpression<TValue>)!,
            _ when typeof(TValue) == typeof(byte) => (MemberExpByte(memberName) as DvlSqlMemberExpression<TValue>)!,
            _ when typeof(TValue) == typeof(decimal) =>
                (MemberExpDecimal(memberName) as DvlSqlMemberExpression<TValue>)!,
            _ when typeof(TValue) == typeof(DateTime) =>
                (MemberExpDateTime(memberName) as DvlSqlMemberExpression<TValue>)!,
            _ => throw new NotImplementedException($"Type: {typeof(TValue)} was not implemented")
        };

    public static DvlSqlGroupByMemberExpressionString GroupByMemberExpString(string memberName) => new(memberName);

    public static DvlSqlGroupByMemberExpressionGuid GroupByMemberExpGuid(string memberName) => new(memberName);

    public static DvlSqlGroupByMemberExpressionInt GroupByMemberExpInt(string memberName) => new(memberName);

    public static DvlSqlGroupByMemberExpressionLong GroupByMemberExpLong(string memberName) => new(memberName);

    public static DvlSqlGroupByMemberExpressionDouble GroupByMemberExpDouble(string memberName) => new(memberName);

    public static DvlSqlGroupByMemberExpressionDecimal GroupByMemberExpDecimal(string memberName) => new(memberName);

    public static DvlSqlGroupByMemberExpressionDateTime GroupByMemberExpDateTime(string memberName) => new(memberName);

    public static DvlSqlGroupByMemberExpressionBool GroupByMemberExpBool(string memberName) => new(memberName);

    public static DvlSqlGroupByMemberExpressionByte GroupByMemberExpByte(string memberName) => new(memberName);

    public static DvlSqlOrExpression OrExp(params DvlSqlBinaryExpression[] innerExpressions) =>
        new(innerExpressions);

    public static DvlSqlInExpression InExp(string parameterName, params DvlSqlExpression[] innerExpressions) =>
        new(parameterName, innerExpressions);

    // NOTE: topNum must be rendered as "LIMIT n" (after ORDER BY) by DvlSqlSelectExpression /
    // DvlSqlFullSelectExpression in the PostgreSQL provider, not as "TOP n".
    public static DvlSqlSelectExpression SelectExp(int? topNum = null) =>
        new(topNum);

    public static DvlSqlSelectExpression SelectExp(int? topNum = null,
        params string[] paramNames) =>
        new(paramNames.ToList(), topNum);

    public static DvlSqlSelectExpression SelectExp(IEnumerable<string> paramNames,
        int? topNum = null) =>
        new(paramNames.ToList(), topNum);

    public static DvlSqlFromExpression FromExp(DvlSqlFullSelectExpression select, string? @as = null)
    {
        if (@as != null)
            select.As = AsExp(@as);
        return select;
    }

    public static DvlSqlFromExpression FromExp(DvlSqlFromWithTableExpression fromWithTable, string? @as = null)
    {
        if (@as != null)
            fromWithTable.As = AsExp(@as);
        return fromWithTable;
    }

    public static DvlSqlFromExpression ValuesExp<T>(T[] values, DvlSqlAsExpression @as) where T : ITuple =>
        new DvlSqlValuesExpression<T>(values, @as);

    public static DvlSqlFromExpression ValuesExp<T>(T[] values, DvlSqlAsExpression @as,
        List<DvlSqlParameter> sqlParameters) where T : ITuple =>
        new DvlSqlValuesExpression<T>(values, @as, sqlParameters);

    public static DvlSqlSelectExpression SelectExp(params string[] paramNames) =>
        new(paramNames);

    public static DvlSqlSelectExpression SelectTopExp(int topNum,
        params string[] paramNames) =>
        new(paramNames.ToList(), topNum);

    public static DvlSqlLikeExpression LikeExp(string field, string pattern) =>
        new(field, pattern);

    public static DvlSqlBinaryExpression NotExp(DvlSqlBinaryExpression binaryExpression) =>
        !binaryExpression;

    public static DvlSqlBinaryExpression
        NotInExp(string parameterName, params DvlSqlExpression[] innerExpressions) =>
        NotExp(InExp(parameterName, innerExpressions));

    public static DvlSqlBinaryExpression NotLikeExp(string field, string pattern) =>
        NotExp(LikeExp(field, pattern));

    public static DvlSqlIsNullExpression IsNullExp(DvlSqlExpression expression) =>
        new(expression);

    public static DvlSqlBinaryExpression IsNotNullExp(DvlSqlExpression expression) => NotExp(IsNullExp(expression));

    public static DvlSqlFullSelectExpression FullSelectExp(
        DvlSqlSelectExpression @select,
        DvlSqlFromExpression @from,
        DvlSqlAsExpression? @as = null,
        List<DvlSqlJoinExpression>? @join = null, DvlSqlWhereExpression? @where = null,
        DvlSqlGroupByExpression? groupBy = null, DvlSqlOrderByExpression? orderBy = null,
        DvlSqlSkipExpression? skip = null) =>
        new(@from, @join, @where, groupBy,
            @select, orderBy, skip, @as);

    public static DvlSqlOrderByExpression OrderByExp(params (string column, Ordering ordering)[] @params) =>
        new(@params);

    public static DvlSqlExistsExpression ExistsExp(DvlSqlFullSelectExpression select) =>
        new(select);

    // OFFSET n ROWS FETCH NEXT m ROWS ONLY is valid in PostgreSQL 13+ as well
    public static DvlSqlSkipExpression SkipExp(int offsetRows, int? fetchNextRows = null) =>
        new(offsetRows, fetchNextRows);

    public static DvlSqlGroupByExpression GroupByExp(params string[] paramNames) =>
        new(paramNames);

    // NOTE: PostgreSQL has no table variables (DECLARE @t TABLE). The provider must render
    // this as a temp table or CTE.
    public static DvlSqlTableDeclarationExpression DeclareTableExp(string name) =>
        new(name);

    // NOTE: PostgreSQL uses "RETURNING col" (no INSERTED./DELETED. prefixes, no INTO @table).
    public static DvlSqlOutputExpression
        OutputExp(DvlSqlTableDeclarationExpression intoTable, params string[] cols) =>
        new(intoTable, cols);

    public static DvlSqlOutputExpression OutputExp(params string[] cols) =>
        new(cols);

    public static DvlSqlValuesExpression<T> ValuesExp<T>(params T[] values) where T : ITuple =>
        new(values);

    public static DvlSqlInnerJoinExpression<T> InnerJoinExp<T>(string tableName,
        string firstTableMatchingCol, string secondTableMatchingCol)
        => new(tableName,
            MemberExp<T>(firstTableMatchingCol) == MemberExp<T>(secondTableMatchingCol));

    public static DvlSqlLeftJoinExpression<T> LeftJoinExp<T>(string tableName,
        string firstTableMatchingCol, string secondTableMatchingCol)
        => new(tableName,
            MemberExp<T>(firstTableMatchingCol) == MemberExp<T>(secondTableMatchingCol));

    public static DvlSqlRightJoinExpression<T> RightJoinExp<T>(string tableName,
        string firstTableMatchingCol, string secondTableMatchingCol)
        => new(tableName,
            MemberExp<T>(firstTableMatchingCol) == MemberExp<T>(secondTableMatchingCol));

    public static DvlSqlFullJoinExpression<T> FullJoinExp<T>(string tableName,
        string firstTableMatchingCol, string secondTableMatchingCol)
        => new(tableName,
            MemberExp<T>(firstTableMatchingCol) == MemberExp<T>(secondTableMatchingCol));

    public static DvlSqlInnerJoinExpression<T> InnerJoinExp<T>(string tableName,
        DvlSqlComparisonExpression<T> comparisonExpression)
        => new(tableName, comparisonExpression);

    public static DvlSqlLeftJoinExpression<T> LeftJoinExp<T>(string tableName,
        DvlSqlComparisonExpression<T> comparisonExpression)
        => new(tableName, comparisonExpression);

    public static DvlSqlRightJoinExpression<T> RightJoinExp<T>(string tableName,
        DvlSqlComparisonExpression<T> comparisonExpression)
        => new(tableName, comparisonExpression);

    public static DvlSqlFullJoinExpression<T> FullJoinExp<T>(string tableName,
        DvlSqlComparisonExpression<T> comparisonExpression)
        => new(tableName, comparisonExpression);

    public static DvlSqlInnerJoinExpression InnerJoinExp(string tableName,
        string firstTableMatchingCol, string secondTableMatchingCol)
        => new(tableName,
            ConstantExpCol(firstTableMatchingCol) == ConstantExpCol(secondTableMatchingCol));

    public static DvlSqlLeftJoinExpression LeftJoinExp(string tableName,
        string firstTableMatchingCol, string secondTableMatchingCol)
        => new(tableName,
            ConstantExpCol(firstTableMatchingCol) == ConstantExpCol(secondTableMatchingCol));

    public static DvlSqlRightJoinExpression RightJoinExp(string tableName,
        string firstTableMatchingCol, string secondTableMatchingCol)
        => new(tableName,
            ConstantExpCol(firstTableMatchingCol) == ConstantExpCol(secondTableMatchingCol));

    public static DvlSqlFullJoinExpression FullJoinExp(string tableName,
        string firstTableMatchingCol, string secondTableMatchingCol)
        => new(tableName,
            ConstantExpCol(firstTableMatchingCol) == ConstantExpCol(secondTableMatchingCol));

    public static DvlSqlInnerJoinExpression InnerJoinExp(string tableName,
        DvlSqlBinaryExpression binaryExpression)
        => new(tableName, binaryExpression);

    public static DvlSqlLeftJoinExpression LeftJoinExp(string tableName,
        DvlSqlBinaryExpression binaryExpression)
        => new(tableName, binaryExpression);

    public static DvlSqlRightJoinExpression RightJoinExp(string tableName,
        DvlSqlBinaryExpression binaryExpression)
        => new(tableName, binaryExpression);

    public static DvlSqlFullJoinExpression FullJoinExp(string tableName,
        DvlSqlBinaryExpression binaryExpression)
        => new(tableName, binaryExpression);

    public static DvlSqlBinaryEmptyExpression EmptyExp() => new();

    #region Aggregate Expressions

    public static DvlSqlMaxExpressionString MaxExpString(string memberName) => new(memberName);

    public static DvlSqlMaxExpressionInt MaxExpInt(string memberName) => new(memberName);

    public static DvlSqlMaxExpressionLong MaxExpLong(string memberName) => new(memberName);

    public static DvlSqlMaxExpressionDouble MaxExpDouble(string memberName) => new(memberName);

    public static DvlSqlMaxExpressionDecimal MaxExpDecimal(string memberName) => new(memberName);

    public static DvlSqlMaxExpressionDateTime MaxExpDateTime(string memberName) => new(memberName);

    public static DvlSqlMaxExpressionBool MaxExpBool(string memberName) => new(memberName);

    public static DvlSqlMaxExpressionByte MaxExpByte(string memberName) => new(memberName);

    public static DvlSqlMinExpressionString MinExpString(string memberName) => new(memberName);

    public static DvlSqlMinExpressionInt MinExpInt(string memberName) => new(memberName);

    public static DvlSqlMinExpressionLong MinExpLong(string memberName) => new(memberName);

    public static DvlSqlMinExpressionDouble MinExpDouble(string memberName) => new(memberName);

    public static DvlSqlMinExpressionDecimal MinExpDecimal(string memberName) => new(memberName);

    public static DvlSqlMinExpressionDateTime MinExpDateTime(string memberName) => new(memberName);

    public static DvlSqlMinExpressionBool MinExpBool(string memberName) => new(memberName);

    public static DvlSqlMinExpressionByte MinExpByte(string memberName) => new(memberName);

    public static DvlSqlCountExpressionString CountAllExpString() => new("*");

    public static DvlSqlCountExpressionString CountExpString(string memberName) => new(memberName);

    public static DvlSqlCountExpressionGuid CountExpGuid(string memberName) => new(memberName);

    public static DvlSqlCountExpressionInt CountExpInt(string memberName) => new(memberName);

    public static DvlSqlCountExpressionLong CountExpLong(string memberName) => new(memberName);

    public static DvlSqlCountExpressionDouble CountExpDouble(string memberName) => new(memberName);

    public static DvlSqlCountExpressionDecimal CountExpDecimal(string memberName) => new(memberName);

    public static DvlSqlCountExpressionDateTime CountExpDateTime(string memberName) => new(memberName);

    public static DvlSqlCountExpressionBool CountExpBool(string memberName) => new(memberName);

    public static DvlSqlCountExpressionByte CountExpByte(string memberName) => new(memberName);

    public static DvlSqlAverageExpressionInt AverageExpInt(string memberName) => new(memberName);

    public static DvlSqlAverageExpressionLong AverageExpLong(string memberName) => new(memberName);

    public static DvlSqlAverageExpressionDouble AverageExpDouble(string memberName) => new(memberName);

    public static DvlSqlAverageExpressionDecimal AverageExpDecimal(string memberName) => new(memberName);

    public static DvlSqlSumExpressionInt SumExpInt(string memberName) => new(memberName);

    public static DvlSqlSumExpressionLong SumExpLong(string memberName) => new(memberName);

    public static DvlSqlSumExpressionDouble SumExpDouble(string memberName) => new(memberName);

    public static DvlSqlSumExpressionDecimal SumExpDecimal(string memberName) => new(memberName);

    #endregion

    #endregion
}