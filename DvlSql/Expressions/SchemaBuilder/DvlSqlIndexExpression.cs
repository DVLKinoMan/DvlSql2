namespace DvlSql.Expressions;

public class DvlSqlIndexExpression(string name, string tableName, string columnName, bool isUnique = false) : DvlSqlSchemaExpression
{
    public string Name { get; } = name;
    public string TableName { get; } = tableName;
    public bool IsUnique { get; set; } = isUnique;
    public string ColumnName { get; } = columnName;
    
    public void Accept(ICreateTableVisitor visitor) => visitor.Visit(this);
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
    
    public static bool operator ==(DvlSqlIndexExpression? left, DvlSqlIndexExpression? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Name == right.Name &&
               left.TableName == right.TableName &&
               left.IsUnique == right.IsUnique &&
               left.ColumnName == right.ColumnName;
    }

    public static bool operator !=(DvlSqlIndexExpression? left, DvlSqlIndexExpression? right)
    {
        return !(left == right);
    }
}