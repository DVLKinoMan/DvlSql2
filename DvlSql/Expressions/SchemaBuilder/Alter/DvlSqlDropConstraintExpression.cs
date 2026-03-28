namespace DvlSql.Expressions;

public class DvlSqlDropConstraintExpression(string name)
{
    public string Name { get; } = name;
}