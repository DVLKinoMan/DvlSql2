namespace DvlSql.Expressions;

public abstract class DvlSqlSumExpression<TResult>(string memberName)
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

public class DvlSqlSumExpressionInt(string memberName) : DvlSqlSumExpression<int>(memberName)
{
    protected bool Equals(DvlSqlSumExpressionInt other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlSumExpressionInt)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlSumExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlSumExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlSumExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlSumExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlSumExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlSumExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionInt lhs,
        DvlSqlSumExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionInt lhs,
        DvlSqlSumExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionInt lhs,
        DvlSqlSumExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionInt lhs,
        DvlSqlSumExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionInt lhs,
        DvlSqlSumExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionInt lhs,
        DvlSqlSumExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<int> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlSumExpressionLong(string memberName) : DvlSqlSumExpression<long>(memberName)
{
    protected bool Equals(DvlSqlSumExpressionLong other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlSumExpressionLong)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlSumExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlSumExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(
        DvlSqlSumExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(
        DvlSqlSumExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(
        DvlSqlSumExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(
        DvlSqlSumExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlSumExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlSumExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlSumExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlSumExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlSumExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlSumExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<long> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlSumExpressionDouble(string memberName) : DvlSqlSumExpression<double>(memberName)
{
    protected bool Equals(DvlSqlSumExpressionDouble other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlSumExpressionDouble)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlSumExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlSumExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(
        DvlSqlSumExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(
        DvlSqlSumExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(
        DvlSqlSumExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(
        DvlSqlSumExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlSumExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlSumExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlSumExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlSumExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlSumExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlSumExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<double> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlSumExpressionDecimal(string memberName) : DvlSqlSumExpression<decimal>(memberName)
{
    protected bool Equals(DvlSqlSumExpressionDecimal other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlSumExpressionDecimal)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlSumExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlSumExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(
        DvlSqlSumExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(
        DvlSqlSumExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(
        DvlSqlSumExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(
        DvlSqlSumExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlSumExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlSumExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlSumExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlSumExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlSumExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlSumExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<decimal> ComparableClone() => throw new NotImplementedException();
}