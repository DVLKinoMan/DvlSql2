namespace DvlSql.Expressions;

public abstract class DvlSqlGroupByMemberExpression<TValue>(string memberName)
    : DvlSqlGroupBySelectableExpression<TValue>(memberName)
{
    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null)
            return false;

        throw new NotImplementedException();
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public override DvlSqlExpression Clone() => ComparableClone();

    public override DvlSqlComparableExpression<TValue> ComparableClone() =>
        throw new NotImplementedException(); //new DvlSqlGroupByMemberExpression<TValue>(MemberName);

    public static DvlSqlGroupByComparisonExpression<TValue> operator ==(DvlSqlGroupByMemberExpression<TValue> lhs,
        DvlSqlGroupByMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<TValue> operator !=(DvlSqlGroupByMemberExpression<TValue> lhs,
        DvlSqlGroupByMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<TValue> operator >(DvlSqlGroupByMemberExpression<TValue> lhs,
        DvlSqlGroupByMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<TValue> operator >=(DvlSqlGroupByMemberExpression<TValue> lhs,
        DvlSqlGroupByMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<TValue> operator <(DvlSqlGroupByMemberExpression<TValue> lhs,
        DvlSqlGroupByMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<TValue> operator <=(DvlSqlGroupByMemberExpression<TValue> lhs,
        DvlSqlGroupByMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<TValue> operator ==(DvlSqlGroupByMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<TValue> operator !=(DvlSqlGroupByMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<TValue> operator >(DvlSqlGroupByMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<TValue> operator >=(DvlSqlGroupByMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<TValue> operator <(DvlSqlGroupByMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<TValue> operator <=(DvlSqlGroupByMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression BaseComparableClone() => ComparableClone();
}

public class DvlSqlGroupByMemberExpressionInt(string memberName) : DvlSqlGroupByMemberExpression<int>(memberName)
{
    protected bool Equals(DvlSqlGroupByMemberExpressionInt other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlGroupByMemberExpressionInt)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByComparisonExpression<int> operator ==(DvlSqlGroupByMemberExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<int> operator !=(DvlSqlGroupByMemberExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<int> operator >(DvlSqlGroupByMemberExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<int> operator >=(DvlSqlGroupByMemberExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<int> operator <(DvlSqlGroupByMemberExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<int> operator <=(DvlSqlGroupByMemberExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<int> operator ==(DvlSqlConstantExpressionInt lhs,
        DvlSqlGroupByMemberExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<int> operator !=(DvlSqlConstantExpressionInt lhs,
        DvlSqlGroupByMemberExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.NotEquality, rhs);

    public static DvlSqlGroupByComparisonExpression<int> operator >(DvlSqlConstantExpressionInt lhs,
        DvlSqlGroupByMemberExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<int> operator >=(DvlSqlConstantExpressionInt lhs,
        DvlSqlGroupByMemberExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<int> operator <(DvlSqlConstantExpressionInt lhs,
        DvlSqlGroupByMemberExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<int> operator <=(DvlSqlConstantExpressionInt lhs,
        DvlSqlGroupByMemberExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}

public class DvlSqlGroupByMemberExpressionString(string memberName) : DvlSqlGroupByMemberExpression<string>(memberName)
{
    protected bool Equals(DvlSqlGroupByMemberExpressionString other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlGroupByMemberExpressionString)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByComparisonExpression<string> operator ==(DvlSqlGroupByMemberExpressionString lhs,
        DvlSqlConstantExpressionString rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<string> operator !=(DvlSqlGroupByMemberExpressionString lhs,
        DvlSqlConstantExpressionString rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<string> operator ==(DvlSqlConstantExpressionString lhs,
        DvlSqlGroupByMemberExpressionString rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<string> operator !=(DvlSqlConstantExpressionString lhs,
        DvlSqlGroupByMemberExpressionString rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);
}

public class DvlSqlGroupByMemberExpressionDateTime(string memberName)
    : DvlSqlGroupByMemberExpression<DateTime>(memberName)
{
    protected bool Equals(DvlSqlGroupByMemberExpressionDateTime other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlGroupByMemberExpressionDateTime)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByComparisonExpression<DateTime> operator ==(DvlSqlGroupByMemberExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<DateTime> operator !=(DvlSqlGroupByMemberExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<DateTime> operator >(DvlSqlGroupByMemberExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<DateTime> operator >=(DvlSqlGroupByMemberExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<DateTime> operator <(DvlSqlGroupByMemberExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<DateTime> operator <=(DvlSqlGroupByMemberExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<DateTime> operator ==(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlGroupByMemberExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<DateTime> operator !=(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlGroupByMemberExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<DateTime> operator >(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlGroupByMemberExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<DateTime> operator >=(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlGroupByMemberExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<DateTime> operator <(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlGroupByMemberExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<DateTime> operator <=(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlGroupByMemberExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}

public class DvlSqlGroupByMemberExpressionDouble(string memberName) : DvlSqlGroupByMemberExpression<double>(memberName)
{
    protected bool Equals(DvlSqlGroupByMemberExpressionDouble other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlGroupByMemberExpressionDouble)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByComparisonExpression<double> operator ==(DvlSqlGroupByMemberExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<double> operator !=(DvlSqlGroupByMemberExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<double> operator >(DvlSqlGroupByMemberExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<double> operator >=(DvlSqlGroupByMemberExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<double> operator <(DvlSqlGroupByMemberExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<double> operator <=(DvlSqlGroupByMemberExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<double> operator ==(DvlSqlConstantExpressionDouble lhs,
        DvlSqlGroupByMemberExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<double> operator !=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlGroupByMemberExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<double> operator >(DvlSqlConstantExpressionDouble lhs,
        DvlSqlGroupByMemberExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<double> operator >=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlGroupByMemberExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<double> operator <(DvlSqlConstantExpressionDouble lhs,
        DvlSqlGroupByMemberExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<double> operator <=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlGroupByMemberExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}

public class DvlSqlGroupByMemberExpressionDecimal(string memberName)
    : DvlSqlGroupByMemberExpression<decimal>(memberName)
{
    protected bool Equals(DvlSqlGroupByMemberExpressionDecimal other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlGroupByMemberExpressionDecimal)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByComparisonExpression<decimal> operator ==(DvlSqlGroupByMemberExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<decimal> operator !=(DvlSqlGroupByMemberExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<decimal> operator >(DvlSqlGroupByMemberExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<decimal> operator >=(DvlSqlGroupByMemberExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<decimal> operator <(DvlSqlGroupByMemberExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<decimal> operator <=(DvlSqlGroupByMemberExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<decimal> operator ==(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlGroupByMemberExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<decimal> operator !=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlGroupByMemberExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<decimal> operator >(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlGroupByMemberExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<decimal> operator >=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlGroupByMemberExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<decimal> operator <(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlGroupByMemberExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<decimal> operator <=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlGroupByMemberExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}

public class DvlSqlGroupByMemberExpressionBool(string memberName) : DvlSqlGroupByMemberExpression<bool>(memberName)
{
    protected bool Equals(DvlSqlGroupByMemberExpressionBool other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlGroupByMemberExpressionBool)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByComparisonExpression<bool> operator ==(DvlSqlGroupByMemberExpressionBool lhs,
        DvlSqlConstantExpressionBool rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<bool> operator !=(DvlSqlGroupByMemberExpressionBool lhs,
        DvlSqlConstantExpressionBool rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<bool> operator ==(DvlSqlConstantExpressionBool lhs,
        DvlSqlGroupByMemberExpressionBool rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<bool> operator !=(DvlSqlConstantExpressionBool lhs,
        DvlSqlGroupByMemberExpressionBool rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);
}

public class DvlSqlGroupByMemberExpressionByte(string memberName) : DvlSqlGroupByMemberExpression<byte>(memberName)
{
    protected bool Equals(DvlSqlGroupByMemberExpressionByte other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlGroupByMemberExpressionByte)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByComparisonExpression<byte> operator ==(DvlSqlGroupByMemberExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<byte> operator !=(DvlSqlGroupByMemberExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<byte> operator >(DvlSqlGroupByMemberExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<byte> operator >=(DvlSqlGroupByMemberExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<byte> operator <(DvlSqlGroupByMemberExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<byte> operator <=(DvlSqlGroupByMemberExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<byte> operator ==(DvlSqlConstantExpressionByte lhs,
        DvlSqlGroupByMemberExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<byte> operator !=(DvlSqlConstantExpressionByte lhs,
        DvlSqlGroupByMemberExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<byte> operator >(DvlSqlConstantExpressionByte lhs,
        DvlSqlGroupByMemberExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<byte> operator >=(DvlSqlConstantExpressionByte lhs,
        DvlSqlGroupByMemberExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<byte> operator <(DvlSqlConstantExpressionByte lhs,
        DvlSqlGroupByMemberExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<byte> operator <=(DvlSqlConstantExpressionByte lhs,
        DvlSqlGroupByMemberExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}

public class DvlSqlGroupByMemberExpressionLong(string memberName) : DvlSqlGroupByMemberExpression<long>(memberName)
{
    protected bool Equals(DvlSqlGroupByMemberExpressionLong other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlGroupByMemberExpressionLong)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlGroupByComparisonExpression<long> operator ==(DvlSqlGroupByMemberExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<long> operator !=(DvlSqlGroupByMemberExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<long> operator >(DvlSqlGroupByMemberExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<long> operator >=(DvlSqlGroupByMemberExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<long> operator <(DvlSqlGroupByMemberExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<long> operator <=(DvlSqlGroupByMemberExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<long> operator ==(DvlSqlConstantExpressionLong lhs,
        DvlSqlGroupByMemberExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlGroupByComparisonExpression<long> operator !=(DvlSqlConstantExpressionLong lhs,
        DvlSqlGroupByMemberExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlGroupByComparisonExpression<long> operator >(DvlSqlConstantExpressionLong lhs,
        DvlSqlGroupByMemberExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlGroupByComparisonExpression<long> operator >=(DvlSqlConstantExpressionLong lhs,
        DvlSqlGroupByMemberExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlGroupByComparisonExpression<long> operator <(DvlSqlConstantExpressionLong lhs,
        DvlSqlGroupByMemberExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlGroupByComparisonExpression<long> operator <=(DvlSqlConstantExpressionLong lhs,
        DvlSqlGroupByMemberExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}