namespace DvlSql.Expressions;

public class DvlSqlForeignKeyExpression
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
}