namespace DvlSql.Expressions;

public abstract class DvlSqlCountExpression(string memberName)
    : DvlSqlAggregateFunctionExpression<long>(memberName)
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

public class DvlSqlCountExpressionInt(string memberName) : DvlSqlCountExpression(memberName)
{
    protected bool Equals(DvlSqlCountExpressionInt other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlCountExpressionInt)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlCountExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlCountExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlCountExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlCountExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlCountExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlCountExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionInt lhs,
        DvlSqlCountExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionInt lhs,
        DvlSqlCountExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionInt lhs,
        DvlSqlCountExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionInt lhs,
        DvlSqlCountExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionInt lhs,
        DvlSqlCountExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionInt lhs,
        DvlSqlCountExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<long> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlCountExpressionLong(string memberName) : DvlSqlCountExpression(memberName)
{
    protected bool Equals(DvlSqlCountExpressionLong other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlCountExpressionLong)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlCountExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlCountExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlCountExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlCountExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlCountExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlCountExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlCountExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlCountExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionLong lhs,
        DvlSqlCountExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionLong lhs,
        DvlSqlCountExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionLong lhs,
        DvlSqlCountExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionLong lhs,
        DvlSqlCountExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<long> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlCountExpressionDouble(string memberName) : DvlSqlCountExpression(memberName)
{
    protected bool Equals(DvlSqlCountExpressionDouble other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlCountExpressionDouble)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlCountExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlCountExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlCountExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlCountExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlCountExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlCountExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlCountExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlCountExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionDouble lhs,
        DvlSqlCountExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlCountExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionDouble lhs,
        DvlSqlCountExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlCountExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<long> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlCountExpressionDecimal(string memberName) : DvlSqlCountExpression(memberName)
{
    protected bool Equals(DvlSqlCountExpressionDecimal other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlCountExpressionDecimal)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlCountExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlCountExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlCountExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlCountExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlCountExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlCountExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlCountExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlCountExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlCountExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlCountExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlCountExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlCountExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<long> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlCountExpressionString(string memberName) : DvlSqlCountExpression(memberName)
{
    protected bool Equals(DvlSqlCountExpressionString other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlCountExpressionString)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlCountExpressionString lhs,
        DvlSqlConstantExpressionString rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlCountExpressionString lhs,
        DvlSqlConstantExpressionString rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionString lhs,
        DvlSqlCountExpressionString rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionString lhs,
        DvlSqlCountExpressionString rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public override DvlSqlComparableExpression<long> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlCountExpressionDateTime(string memberName) : DvlSqlCountExpression(memberName)
{
    protected bool Equals(DvlSqlCountExpressionDateTime other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlCountExpressionDateTime)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlCountExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlCountExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlCountExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlCountExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlCountExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlCountExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionDateTime lhs,
        DvlSqlCountExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionDateTime lhs,
        DvlSqlCountExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlCountExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlCountExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlCountExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlCountExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<long> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlCountExpressionByte(string memberName) : DvlSqlCountExpression(memberName)
{
    protected bool Equals(DvlSqlCountExpressionByte other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlCountExpressionByte)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlCountExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlCountExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlCountExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlCountExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlCountExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlCountExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionByte lhs,
        DvlSqlCountExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionByte lhs,
        DvlSqlCountExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionByte lhs,
        DvlSqlCountExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionByte lhs,
        DvlSqlCountExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionByte lhs,
        DvlSqlCountExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionByte lhs,
        DvlSqlCountExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<long> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlCountExpressionBool(string memberName) : DvlSqlCountExpression(memberName)
{
    protected bool Equals(DvlSqlCountExpressionBool other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlCountExpressionBool)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlCountExpressionBool lhs,
        DvlSqlConstantExpressionBool rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlCountExpressionBool lhs,
        DvlSqlConstantExpressionBool rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionBool lhs,
        DvlSqlCountExpressionBool rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionBool lhs,
        DvlSqlCountExpressionBool rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public override DvlSqlComparableExpression<long> ComparableClone() => throw new NotImplementedException();
}