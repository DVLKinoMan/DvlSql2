namespace DvlSql.Expressions;

public class DvlSqlCreateTableExpression(string name, string? associatedName = null)
{
    public string Name { get; } = name;
    public string? AssociatedName { get; set; } = associatedName;
    public List<DvlSqlCreateColumnExpression> ColumnExpressions = [];

    public void Accept(ICreateTableVisitor visitor) => visitor.Visit(this);
    
    public static bool operator ==(DvlSqlCreateTableExpression? left, DvlSqlCreateTableExpression? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return (left.AssociatedName is not null && left.AssociatedName == right.AssociatedName)
            || (left.AssociatedName is null && right.AssociatedName is null && left.Name == right.Name);
    }
    
    public static bool operator !=(DvlSqlCreateTableExpression? left, DvlSqlCreateTableExpression? right) => !(left == right);

    public override bool Equals(object? obj) => obj is DvlSqlCreateTableExpression other && this == other;

    public override int GetHashCode() => HashCode.Combine(Name, AssociatedName);
}