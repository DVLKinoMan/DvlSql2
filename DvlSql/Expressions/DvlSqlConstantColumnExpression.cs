namespace DvlSql.Expressions;

public class DvlSqlConstantColumnExpression(string value) : DvlSqlComparableExpression
{
    protected bool Equals(DvlSqlConstantColumnExpression other)
    {
        return Value == other.Value && Equals(Parameter, other.Parameter);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlConstantColumnExpression)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, Parameter);
    }

    public string Value { get; } = value;

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => new DvlSqlConstantColumnExpression(Value);

    public static DvlSqlComparisonExpression operator ==(DvlSqlConstantColumnExpression lhs,
        int rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(DvlSqlConstantColumnExpression lhs, int rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator >(DvlSqlConstantColumnExpression lhs, int rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression operator >=(DvlSqlConstantColumnExpression lhs, int rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression operator <(DvlSqlConstantColumnExpression lhs, int rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression operator <=(DvlSqlConstantColumnExpression lhs, int rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression operator ==(int lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(int lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator >(int lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression operator >=(int lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression operator <(int lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression operator <=(int lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression operator ==(DvlSqlConstantColumnExpression lhs, long rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(DvlSqlConstantColumnExpression lhs, long rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator >(DvlSqlConstantColumnExpression lhs, long rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression operator >=(DvlSqlConstantColumnExpression lhs, long rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression operator <(DvlSqlConstantColumnExpression lhs, long rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression operator <=(DvlSqlConstantColumnExpression lhs, long rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression operator ==(long lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(long lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator >(long lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression operator >=(long lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression operator <(long lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression operator <=(long lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression operator ==(DvlSqlConstantColumnExpression lhs, Guid rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(DvlSqlConstantColumnExpression lhs, Guid rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator ==(Guid lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(Guid lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator ==(DvlSqlConstantColumnExpression lhs, string rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(DvlSqlConstantColumnExpression lhs, string rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator ==(string lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(string lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator ==(DvlSqlConstantColumnExpression lhs, DateTime rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(DvlSqlConstantColumnExpression lhs, DateTime rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator >(DvlSqlConstantColumnExpression lhs, DateTime rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression operator >=(DvlSqlConstantColumnExpression lhs, DateTime rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression operator <(DvlSqlConstantColumnExpression lhs, DateTime rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression operator <=(DvlSqlConstantColumnExpression lhs, DateTime rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression operator ==(DateTime lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(DateTime lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator >(DateTime lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression operator >=(DateTime lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression operator <(DateTime lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression operator <=(DateTime lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression operator ==(DvlSqlConstantColumnExpression lhs, double rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(DvlSqlConstantColumnExpression lhs, double rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator >(DvlSqlConstantColumnExpression lhs, double rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression operator >=(DvlSqlConstantColumnExpression lhs, double rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression operator <(DvlSqlConstantColumnExpression lhs, double rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression operator <=(DvlSqlConstantColumnExpression lhs, double rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression operator ==(double lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(double lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator >(double lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression operator >=(double lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression operator <(double lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression operator <=(double lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression operator ==(DvlSqlConstantColumnExpression lhs,
        decimal rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(DvlSqlConstantColumnExpression lhs, decimal rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator >(DvlSqlConstantColumnExpression lhs, decimal rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression operator >=(DvlSqlConstantColumnExpression lhs, decimal rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression operator <(DvlSqlConstantColumnExpression lhs, decimal rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression operator <=(DvlSqlConstantColumnExpression lhs, decimal rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression operator ==(decimal lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(decimal lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator >(decimal lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression operator >=(decimal lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression operator <(decimal lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression operator <=(decimal lhs, DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression operator ==(DvlSqlConstantColumnExpression lhs,
        DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression operator !=(DvlSqlConstantColumnExpression lhs,
        DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression operator >(DvlSqlConstantColumnExpression lhs,
        DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression operator >=(DvlSqlConstantColumnExpression lhs,
        DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression operator <(DvlSqlConstantColumnExpression lhs,
        DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression operator <=(DvlSqlConstantColumnExpression lhs,
        DvlSqlConstantColumnExpression rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlParameter? Parameter { get; } = null;
    public override DvlSqlComparableExpression BaseComparableClone() => new DvlSqlConstantColumnExpression(Value);
}