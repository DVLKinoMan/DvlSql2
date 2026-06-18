namespace DvlSql.Expressions;

public class DvlSqlUniqueExpression(string name, string columnName) : DvlSqlSchemaExpression
{
    public string Name { get; } = name;
    public string ColumnName { get; } = columnName;
    
    public void Accept(ICreateTableVisitor visitor) => visitor.Visit(this);
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
    
    public static bool operator ==(DvlSqlUniqueExpression? left, DvlSqlUniqueExpression? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Name == right.Name &&
               left.ColumnName == right.ColumnName;
    }

    public static bool operator !=(DvlSqlUniqueExpression? left, DvlSqlUniqueExpression? right)
    {
        return !(left == right);
    }
}