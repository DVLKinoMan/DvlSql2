namespace DvlSql.Expressions;

public class DvlSqlDefaultExpression(string name, string value)
{
    public string Name { get; } = name;
    public string Value { get; } = value;
    
    public void Accept(ISchemaBuilderVisitor visitor) => visitor.Visit(this);
}