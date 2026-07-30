namespace DvlSql.Expressions;

public class DvlSqlPrimaryKeyExpression(string name, string columnName) : DvlSqlSchemaExpression
{
    public string Name { get; } = name;
    public string ColumnName { get; } = columnName;
    
    public void Accept(ICreateTableVisitor visitor) => visitor.Visit(this);
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
    
    public static bool operator ==(DvlSqlPrimaryKeyExpression? left, DvlSqlPrimaryKeyExpression? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Name == right.Name &&
               left.ColumnName == right.ColumnName;
    }

    public static bool operator !=(DvlSqlPrimaryKeyExpression? left, DvlSqlPrimaryKeyExpression? right)
    {
        return !(left == right);
    }
    
    public override void Visit(ICodeBuilder visitor) => visitor.Visit(this);
}