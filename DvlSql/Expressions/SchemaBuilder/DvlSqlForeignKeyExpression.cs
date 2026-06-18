namespace DvlSql.Expressions;

public class DvlSqlForeignKeyExpression : DvlSqlSchemaExpression
{
    public DvlSqlForeignKeyExpression(string name, string columnName, string referenceTableName, string referenceColumnName)
    {
        Name = name;
        ReferenceTableName = referenceTableName;
        ReferenceColumnName = referenceColumnName;
        ColumnName = columnName;
    }
    
    public DvlSqlForeignKeyExpression(string name, string associatedTableName,
        string columnName, string referenceTableName, 
        string associatedColumnName, string referenceColumnName)
    {
        Name = name;
        AssociatedTableName = associatedTableName;
        ReferenceTableName = referenceTableName;
        ReferenceColumnName = referenceColumnName;
        AssociatedColumnName = associatedColumnName;
        ColumnName = columnName;
    }

    public string Name { get; }
    public string? AssociatedTableName { get; }
    public string ReferenceTableName { get; }
    public string ReferenceColumnName { get; }
    public string? AssociatedColumnName { get; }
    public string ColumnName { get; }
    
    public void Accept(ICreateTableVisitor visitor) => visitor.Visit(this);
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
    
    public static bool operator ==(DvlSqlForeignKeyExpression? left, DvlSqlForeignKeyExpression? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Name == right.Name &&
               left.AssociatedTableName == right.AssociatedTableName &&
               left.ReferenceTableName == right.ReferenceTableName &&
               left.ReferenceColumnName == right.ReferenceColumnName &&
               left.AssociatedColumnName == right.AssociatedColumnName &&
               left.ColumnName == right.ColumnName;
    }

    public static bool operator !=(DvlSqlForeignKeyExpression? left, DvlSqlForeignKeyExpression? right)
    {
        return !(left == right);
    }
}