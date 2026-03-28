namespace DvlSql.Expressions;

public class DvlSqlPrimaryKeyExpression(string name)
{
    public string Name { get; } = name;
    
    public void Accept(ISchemaBuilderVisitor visitor) => visitor.Visit(this);
}