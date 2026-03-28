namespace DvlSql.Expressions;

public class DvlSqlIndexExpression(string name, string tableName, string columnName, bool isUnique = false)
{
    public string Name { get; } = name;
    public string TableName { get; } = tableName;
    public bool IsUnique { get; set; } = isUnique;
    public string ColumnName { get; } = columnName;
    
    public void Accept(ISchemaBuilderVisitor visitor) => visitor.Visit(this);
}