namespace DvlSql.Expressions;

public class DvlSqlUniqueExpression(string name, string columnName)
{
    public string Name { get; } = name;
    public string ColumnName { get; } = columnName;
    
    public void Accept(ICreateTableVisitor visitor) => visitor.Visit(this);
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
}