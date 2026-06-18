namespace DvlSql.Expressions;

public class DvlSqlDefaultExpression(string name, string value, string columnName) : DvlSqlSchemaExpression
{
    public string Name { get; } = name;
    public string Value { get; } = value;
    public string ColumnName { get; } = columnName;
    
    public void Accept(ICreateTableVisitor visitor) => visitor.Visit(this);
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
    
    public static bool operator ==(DvlSqlDefaultExpression? left, DvlSqlDefaultExpression? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Name == right.Name &&
               left.Value == right.Value &&
               left.ColumnName == right.ColumnName;
    }

    public static bool operator !=(DvlSqlDefaultExpression? left, DvlSqlDefaultExpression? right)
    {
        return !(left == right);
    }
}