namespace DvlSql.Expressions;

public class DvlSqlDropTableExpression(string name)
{
    public string Name { get; } = name;
    
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
}