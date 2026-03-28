namespace DvlSql.Expressions;

public class DvlSqlDropIndexExpression(string name)
{
    public string Name { get; } = name;
}