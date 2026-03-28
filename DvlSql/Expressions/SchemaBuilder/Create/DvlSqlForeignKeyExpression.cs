namespace DvlSql.Expressions;

public class DvlSqlForeignKeyExpression(string name, string value)
{
    public string Name { get; } = name;
    public string Value { get; } = value;
}