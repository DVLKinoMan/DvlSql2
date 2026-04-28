namespace DvlSql.Expressions;

public class DvlSqlDropConstraintExpression(string name, string? associatedName = null)
{
    public string Name { get; } = name;
    public string? AssociatedName { get; set; } = associatedName;
    
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
    
    public static bool operator ==(DvlSqlDropConstraintExpression? left, DvlSqlDropConstraintExpression? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return (left.AssociatedName is not null && left.AssociatedName == right.AssociatedName)
               || (left.AssociatedName is null && right.AssociatedName is null && left.Name == right.Name);
    }
    
    public static bool operator !=(DvlSqlDropConstraintExpression? left, DvlSqlDropConstraintExpression? right) => !(left == right);

    public override bool Equals(object? obj) => obj is DvlSqlDropConstraintExpression other && this == other;

    public override int GetHashCode() => HashCode.Combine(Name, AssociatedName);
}