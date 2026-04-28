namespace DvlSql.Expressions;

public class DvlSqlRenameTableExpression(string oldTableName, string newTableName, string? associatedName = null)
{
    public string OldTableName { get; } = oldTableName;
    public string NewTableName { get; } = newTableName;
    public string? AssociatedName { get; set; } = associatedName;
    
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
    
    public static bool operator ==(DvlSqlRenameTableExpression? left, DvlSqlRenameTableExpression? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return (left.AssociatedName is not null && left.AssociatedName == right.AssociatedName)
               || (left.AssociatedName is null && right.AssociatedName is null && left.OldTableName == right.OldTableName && left.NewTableName == right.NewTableName);
    }
    
    public static bool operator !=(DvlSqlRenameTableExpression? left, DvlSqlRenameTableExpression? right) => !(left == right);

    public override bool Equals(object? obj) => obj is DvlSqlRenameTableExpression other && this == other;

    public override int GetHashCode() => HashCode.Combine(OldTableName, NewTableName, AssociatedName);
}