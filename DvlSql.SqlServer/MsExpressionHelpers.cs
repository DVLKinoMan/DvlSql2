using System.Exts;
using System.Globalization;
using DvlSql.Expressions;

namespace DvlSql;

public static class MsExpressionHelpers
{
    public static string BoolOrExp(string field) => $"BOOL_OR({field})";
    
    public static string StringAggExp(string field, string seperator) => $"STRING_AGG({field}, '{seperator}')";
    
    public static string AnyExp(string param) => $"Any({param})";

    public static string MonthExp(string param) => $"MONTH({param})";
    public static string DayExp(string param) => $"DAY({param})";
    public static string YearExp(string param) => $"YEAR({param})";
    public static string GetDateExp() => $"GETDATE()";
    public static string IsNullExp(string param1, string param2) => $"ISNULL({param1}, {param2})";

    public static string DateDiffExp(string interval, string starting, string ending) =>
        $"DATEDIFF({interval}, {starting}, {ending})";

    public static string ConvertExp(string type, string name) => $"CONVERT({type}, {name})";
}