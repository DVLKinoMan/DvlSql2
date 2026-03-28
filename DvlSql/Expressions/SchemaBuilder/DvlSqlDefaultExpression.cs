namespace DvlSql.Expressions;

public class DvlSqlDefaultExpression(string name, string value, string columnName)
{
    public string Name { get; } = name;
    public string Value { get; } = value;
    public string ColumnName { get; } = columnName;
    
    public void Accept(ICreateTableVisitor visitor) => visitor.Visit(this);
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
}