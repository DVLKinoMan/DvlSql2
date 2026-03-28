namespace DvlSql.Expressions;

public class DvlSqlIndexExpression(string name, string tableName, string columnName, bool isUnique = false)
{
    public string Name { get; } = name;
    public string TableName { get; } = tableName;
    public bool IsUnique { get; set; } = isUnique;
    public string ColumnName { get; } = columnName;
    
    public void Accept(ICreateTableVisitor visitor) => visitor.Visit(this);
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
}