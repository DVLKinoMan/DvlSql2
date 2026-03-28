namespace DvlSql.Expressions;

public class DvlSqlForeignKeyExpression(string name, string referenceTableName, string referenceColumnName)
{
    public string Name { get; } = name;
    public string ReferenceTableName { get; } = referenceTableName;
    public string ReferenceColumnName { get; } = referenceColumnName;
    
    public void Accept(ISchemaBuilderVisitor visitor) => visitor.Visit(this);
}