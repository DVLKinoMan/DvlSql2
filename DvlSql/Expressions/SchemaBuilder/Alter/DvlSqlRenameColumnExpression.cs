namespace DvlSql.Expressions;

public class DvlSqlRenameColumnExpression(string oldColumnName, string newColumnName, string? associatedName = null)
{
    public string OldColumnName { get; } = oldColumnName;
    public string NewColumnName { get; } = newColumnName;
    public string? AssociatedName { get; set; } = associatedName;
    
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
    
    public static bool operator ==(DvlSqlRenameColumnExpression? left, DvlSqlRenameColumnExpression? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return (left.AssociatedName is not null && left.AssociatedName == right.AssociatedName)
               || (left.AssociatedName is null && right.AssociatedName is null && left.OldColumnName == right.OldColumnName && left.NewColumnName == right.NewColumnName);
    }
    
    public static bool operator !=(DvlSqlRenameColumnExpression? left, DvlSqlRenameColumnExpression? right) => !(left == right);

    public override bool Equals(object? obj) => obj is DvlSqlRenameColumnExpression other && this == other;

    public override int GetHashCode() => HashCode.Combine(OldColumnName, NewColumnName, AssociatedName);
}