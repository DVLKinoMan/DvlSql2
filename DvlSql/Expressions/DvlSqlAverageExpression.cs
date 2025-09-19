namespace DvlSql.Expressions;

public abstract class DvlSqlAverageExpression<TResult>(string memberName)
    : DvlSqlAggregateFunctionExpression<TResult>(memberName)
{
    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone()
    {
        throw new NotImplementedException();
    }

    public override DvlSqlComparableExpression BaseComparableClone()
    {
        throw new NotImplementedException();
    }

    public override DvlSqlParameter? Parameter { get; }
}

public class DvlSqlAverageExpressionInt(string memberName) : DvlSqlAverageExpression<int>(memberName)
{
    protected bool Equals(DvlSqlAverageExpressionInt other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlAverageExpressionInt)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlAverageExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlAverageExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlAverageExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlAverageExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlAverageExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlAverageExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionInt lhs,
        DvlSqlAverageExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs, SqlComparisonOperator.Equality,
            rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionInt lhs,
        DvlSqlAverageExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs, SqlComparisonOperator.Different,
            rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionInt lhs,
        DvlSqlAverageExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionInt lhs,
        DvlSqlAverageExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionInt lhs,
        DvlSqlAverageExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionInt lhs,
        DvlSqlAverageExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<int> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlAverageExpressionLong(string memberName) : DvlSqlAverageExpression<long>(memberName)
{
    protected bool Equals(DvlSqlAverageExpressionLong other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlAverageExpressionLong)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlAverageExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlAverageExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlAverageExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlAverageExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlAverageExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlAverageExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlAverageExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlAverageExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionLong lhs,
        DvlSqlAverageExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionLong lhs,
        DvlSqlAverageExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionLong lhs,
        DvlSqlAverageExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionLong lhs,
        DvlSqlAverageExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<long> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlAverageExpressionDouble(string memberName) : DvlSqlAverageExpression<double>(memberName)
{
    protected bool Equals(DvlSqlAverageExpressionDouble other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlAverageExpressionDouble)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlAverageExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlAverageExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlAverageExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlAverageExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlAverageExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlAverageExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlAverageExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlAverageExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionDouble lhs,
        DvlSqlAverageExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlAverageExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionDouble lhs,
        DvlSqlAverageExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlAverageExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<double> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlAverageExpressionDecimal(string memberName) : DvlSqlAverageExpression<decimal>(memberName)
{
    protected bool Equals(DvlSqlAverageExpressionDecimal other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlAverageExpressionDecimal)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlAverageExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlAverageExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlAverageExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlAverageExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlAverageExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlAverageExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlAverageExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlAverageExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlAverageExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlAverageExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlAverageExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlAverageExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<decimal> ComparableClone() => throw new NotImplementedException();
}