namespace DvlSql.Expressions;

public abstract class DvlSqlMaxExpression<TResult>(string memberName)
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

public class DvlSqlMaxExpressionInt(string memberName) : DvlSqlMaxExpression<int>(memberName)
{
    protected bool Equals(DvlSqlMaxExpressionInt other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMaxExpressionInt)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMaxExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMaxExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlMaxExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlMaxExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlMaxExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlMaxExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionInt lhs,
        DvlSqlMaxExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionInt lhs,
        DvlSqlMaxExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionInt lhs,
        DvlSqlMaxExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionInt lhs,
        DvlSqlMaxExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionInt lhs,
        DvlSqlMaxExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionInt lhs,
        DvlSqlMaxExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<int> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMaxExpressionLong(string memberName) : DvlSqlMaxExpression<long>(memberName)
{
    protected bool Equals(DvlSqlMaxExpressionLong other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMaxExpressionLong)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMaxExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMaxExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlMaxExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlMaxExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlMaxExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlMaxExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlMaxExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlMaxExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionLong lhs,
        DvlSqlMaxExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionLong lhs,
        DvlSqlMaxExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionLong lhs,
        DvlSqlMaxExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionLong lhs,
        DvlSqlMaxExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<long> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMaxExpressionDouble(string memberName) : DvlSqlMaxExpression<double>(memberName)
{
    protected bool Equals(DvlSqlMaxExpressionDouble other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMaxExpressionDouble)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMaxExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMaxExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlMaxExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlMaxExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlMaxExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlMaxExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlMaxExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlMaxExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMaxExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMaxExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMaxExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMaxExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<double> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMaxExpressionDecimal(string memberName) : DvlSqlMaxExpression<decimal>(memberName)
{
    protected bool Equals(DvlSqlMaxExpressionDecimal other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMaxExpressionDecimal)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMaxExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMaxExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlMaxExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlMaxExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlMaxExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlMaxExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMaxExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMaxExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMaxExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMaxExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMaxExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMaxExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<decimal> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMaxExpressionString(string memberName) : DvlSqlMaxExpression<string>(memberName)
{
    protected bool Equals(DvlSqlMaxExpressionString other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMaxExpressionString)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMaxExpressionString lhs,
        DvlSqlConstantExpressionString rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMaxExpressionString lhs,
        DvlSqlConstantExpressionString rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionString lhs,
        DvlSqlMaxExpressionString rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionString lhs,
        DvlSqlMaxExpressionString rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public override DvlSqlComparableExpression<string> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMaxExpressionDateTime(string memberName) : DvlSqlMaxExpression<DateTime>(memberName)
{
    protected bool Equals(DvlSqlMaxExpressionDateTime other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMaxExpressionDateTime)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMaxExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMaxExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlMaxExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlMaxExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlMaxExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlMaxExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMaxExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMaxExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMaxExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMaxExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMaxExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMaxExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<DateTime> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMaxExpressionByte(string memberName) : DvlSqlMaxExpression<byte>(memberName)
{
    protected bool Equals(DvlSqlMaxExpressionByte other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMaxExpressionByte)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMaxExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMaxExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlMaxExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlMaxExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlMaxExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlMaxExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionByte lhs,
        DvlSqlMaxExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionByte lhs,
        DvlSqlMaxExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionByte lhs,
        DvlSqlMaxExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionByte lhs,
        DvlSqlMaxExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionByte lhs,
        DvlSqlMaxExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionByte lhs,
        DvlSqlMaxExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<byte> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMaxExpressionBool(string memberName) : DvlSqlMaxExpression<bool>(memberName)
{
    protected bool Equals(DvlSqlMaxExpressionBool other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMaxExpressionBool)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMaxExpressionBool lhs,
        DvlSqlConstantExpressionBool rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMaxExpressionBool lhs,
        DvlSqlConstantExpressionBool rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionBool lhs,
        DvlSqlMaxExpressionBool rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionBool lhs,
        DvlSqlMaxExpressionBool rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public override DvlSqlComparableExpression<bool> ComparableClone() => throw new NotImplementedException();
}