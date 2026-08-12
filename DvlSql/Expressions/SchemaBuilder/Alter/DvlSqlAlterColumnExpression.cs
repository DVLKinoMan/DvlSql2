namespace DvlSql.Expressions;

public class DvlSqlAlterColumnExpression(string name, string? associatedName = null) 
    : DvlSqlColumnExpression(name, associatedName)
{
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
    
    public static bool operator ==(DvlSqlAlterColumnExpression? left, DvlSqlAlterColumnExpression? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return (left.AssociatedName is not null && left.AssociatedName == right.AssociatedName)
               || (left.AssociatedName is null && right.AssociatedName is null && left.Name == right.Name);
    }
    
    public static bool operator !=(DvlSqlAlterColumnExpression? left, DvlSqlAlterColumnExpression? right) => !(left == right);

    public override bool Equals(object? obj) => obj is DvlSqlAlterColumnExpression other && this == other;

    public override int GetHashCode() => HashCode.Combine(Name, AssociatedName);
}