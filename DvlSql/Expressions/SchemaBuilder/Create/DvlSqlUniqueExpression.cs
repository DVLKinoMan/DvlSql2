namespace DvlSql.Expressions;

public class DvlSqlUniqueExpression(string name, bool value)
{
    public string Name { get; } = name;
    public bool Value { get; } = value;
}