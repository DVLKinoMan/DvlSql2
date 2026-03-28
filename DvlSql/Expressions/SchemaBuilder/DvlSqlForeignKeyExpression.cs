namespace DvlSql.Expressions;

public class DvlSqlForeignKeyExpression(string name, string columnName, string referenceTableName, string referenceColumnName)
{
    public string Name { get; } = name;
    public string ReferenceTableName { get; } = referenceTableName;
    public string ReferenceColumnName { get; } = referenceColumnName;
    public string ColumnName { get; } = columnName;
    
    public void Accept(ICreateTableVisitor visitor) => visitor.Visit(this);
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
}