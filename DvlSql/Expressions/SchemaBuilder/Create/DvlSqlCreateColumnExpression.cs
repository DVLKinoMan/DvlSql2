using System.Data;

namespace DvlSql.Expressions;

public class DvlSqlCreateColumnExpression(string name, string? associatedName = null) : DvlSqlColumnExpression(name)
{
    public string? AssociatedName { get; set; } = associatedName;
    public void Accept(ICreateTableVisitor visitor) => visitor.Visit(this);
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
    
    public static bool operator ==(DvlSqlCreateColumnExpression? left, DvlSqlCreateColumnExpression? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return (left.AssociatedName is not null && left.AssociatedName == right.AssociatedName)
               || (left.AssociatedName is null && right.AssociatedName is null && left.Name == right.Name);
    }
    
    public static bool operator !=(DvlSqlCreateColumnExpression? left, DvlSqlCreateColumnExpression? right) => !(left == right);

    public override bool Equals(object? obj) => obj is DvlSqlCreateColumnExpression other && this == other;

    public override int GetHashCode() => HashCode.Combine(Name, AssociatedName);
}