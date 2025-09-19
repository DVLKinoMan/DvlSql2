namespace DvlSql.Expressions;

public abstract class DvlSqlMinExpression<TResult>(string memberName)
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

public class DvlSqlMinExpressionInt(string memberName) : DvlSqlMinExpression<int>(memberName)
{
    protected bool Equals(DvlSqlMinExpressionInt other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMinExpressionInt)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    // Operator overloads for DvlSqlMinExpressionInt with DvlSqlConstantExpressionInt
    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMinExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMinExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlMinExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlMinExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlMinExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlMinExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionInt lhs,
        DvlSqlMinExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionInt lhs,
        DvlSqlMinExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionInt lhs,
        DvlSqlMinExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionInt lhs,
        DvlSqlMinExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionInt lhs,
        DvlSqlMinExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionInt lhs,
        DvlSqlMinExpressionInt rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<int> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMinExpressionLong(string memberName) : DvlSqlMinExpression<long>(memberName)
{
    protected bool Equals(DvlSqlMinExpressionLong other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMinExpressionLong)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMinExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMinExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlMinExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlMinExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlMinExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlMinExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlMinExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionLong lhs,
        DvlSqlMinExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionLong lhs,
        DvlSqlMinExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionLong lhs,
        DvlSqlMinExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionLong lhs,
        DvlSqlMinExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionLong lhs,
        DvlSqlMinExpressionLong rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<long> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMinExpressionDouble(string memberName) : DvlSqlMinExpression<double>(memberName)
{
    protected bool Equals(DvlSqlMinExpressionDouble other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMinExpressionDouble)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMinExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMinExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlMinExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlMinExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlMinExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlMinExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlMinExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionDouble lhs,
        DvlSqlMinExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMinExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMinExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMinExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMinExpressionDouble rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<double> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMinExpressionDecimal(string memberName) : DvlSqlMinExpression<decimal>(memberName)
{
    protected bool Equals(DvlSqlMinExpressionDecimal other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMinExpressionDecimal)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMinExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMinExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlMinExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlMinExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlMinExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlMinExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMinExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMinExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMinExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMinExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMinExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMinExpressionDecimal rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<decimal> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMinExpressionString(string memberName) : DvlSqlMinExpression<string>(memberName)
{
    protected bool Equals(DvlSqlMinExpressionString other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMinExpressionString)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMinExpressionString lhs,
        DvlSqlConstantExpressionString rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMinExpressionString lhs,
        DvlSqlConstantExpressionString rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionString lhs,
        DvlSqlMinExpressionString rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionString lhs,
        DvlSqlMinExpressionString rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public override DvlSqlComparableExpression<string> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMinExpressionDateTime(string memberName) : DvlSqlMinExpression<DateTime>(memberName)
{
    protected bool Equals(DvlSqlMinExpressionDateTime other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMinExpressionDateTime)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMinExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMinExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlMinExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlMinExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlMinExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlMinExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMinExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMinExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMinExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMinExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMinExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMinExpressionDateTime rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<DateTime> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMinExpressionByte(string memberName) : DvlSqlMinExpression<byte>(memberName)
{
    protected bool Equals(DvlSqlMinExpressionByte other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMinExpressionByte)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMinExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMinExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlMinExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlMinExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlMinExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlMinExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionByte lhs,
        DvlSqlMinExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionByte lhs,
        DvlSqlMinExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator >(DvlSqlConstantExpressionByte lhs,
        DvlSqlMinExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByBinaryExpression operator >=(DvlSqlConstantExpressionByte lhs,
        DvlSqlMinExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByBinaryExpression operator <(DvlSqlConstantExpressionByte lhs,
        DvlSqlMinExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByBinaryExpression operator <=(DvlSqlConstantExpressionByte lhs,
        DvlSqlMinExpressionByte rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression<byte> ComparableClone() => throw new NotImplementedException();
}

public class DvlSqlMinExpressionBool(string memberName) : DvlSqlMinExpression<bool>(memberName)
{
    protected bool Equals(DvlSqlMinExpressionBool other) => other.MemberName == MemberName;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMinExpressionBool)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlMinExpressionBool lhs,
        DvlSqlConstantExpressionBool rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlMinExpressionBool lhs,
        DvlSqlConstantExpressionBool rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByBinaryExpression operator ==(
        DvlSqlConstantExpressionBool lhs,
        DvlSqlMinExpressionBool rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByBinaryExpression operator !=(
        DvlSqlConstantExpressionBool lhs,
        DvlSqlMinExpressionBool rhs) =>
        new DvlSqlGroupByComparisonExpression(lhs,
            SqlComparisonOperator.Different, rhs);

    public override DvlSqlComparableExpression<bool> ComparableClone() => throw new NotImplementedException();
}