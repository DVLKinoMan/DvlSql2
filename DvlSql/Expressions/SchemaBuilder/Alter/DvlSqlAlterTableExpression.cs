namespace DvlSql.Expressions;

public class DvlSqlAlterTableExpression(string name, string? associatedName = null)
{
    public string Name { get; } = name;
    public string? AssociatedName { get; set; } = associatedName;
    public DvlSqlAlterColumnExpression? AlterColumnExpression { get; set; }
    public DvlSqlCreateColumnExpression? AddColumnExpression { get; set; }
    public DvlSqlDropConstraintExpression? DropConstraintExpression { get; set; }
    public DvlSqlDropIndexExpression? DropIndexExpression { get; set; }
    public DvlSqlRenameColumnExpression? RenameColumnExpression { get; set; }
    public DvlSqlDropColumnExpression? DropColumnExpression { get; set; }
    
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
    
    public static bool operator ==(DvlSqlAlterTableExpression? left, DvlSqlAlterTableExpression? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return (left.AssociatedName is not null && left.AssociatedName == right.AssociatedName)
               || (left.AssociatedName is null && right.AssociatedName is null && left.Name == right.Name);
    }
    
    public static bool operator !=(DvlSqlAlterTableExpression? left, DvlSqlAlterTableExpression? right) => !(left == right);

    public override bool Equals(object? obj) => obj is DvlSqlAlterTableExpression other && this == other;

    public override int GetHashCode() => HashCode.Combine(Name, AssociatedName);
}