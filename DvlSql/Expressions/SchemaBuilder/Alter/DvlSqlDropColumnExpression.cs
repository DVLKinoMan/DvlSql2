namespace DvlSql.Expressions;

public class DvlSqlDropColumnExpression(string name)
{
    public string Name { get; } = name;
    
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
}