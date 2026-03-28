namespace DvlSql.Expressions;

public class DvlSqlIndexExpression(string name, bool value)
{
    public string Name { get; } = name;
    public bool Value { get; } = value;
}