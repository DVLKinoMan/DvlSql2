namespace DvlSql.Expressions;

public abstract class DvlSqlMemberExpression<TValue>(string memberName) : DvlSqlComparableExpression<TValue>
{
    public string MemberName { get; init; } = memberName;

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
        throw new NotImplementedException(); //new DvlSqlMemberExpression<TValue>(MemberName);

    public static DvlSqlComparisonExpression<TValue> operator ==(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<TValue> operator !=(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<TValue> operator >(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<TValue> operator >=(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<TValue> operator <(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<TValue> operator <=(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression<TValue> operator ==(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<TValue> operator !=(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<TValue> operator >(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<TValue> operator >=(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<TValue> operator <(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<TValue> operator <=(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public override DvlSqlComparableExpression BaseComparableClone() => ComparableClone();
}

public class DvlSqlMemberExpressionInt(string memberName) : DvlSqlMemberExpression<int>(memberName)
{
    protected bool Equals(DvlSqlMemberExpressionInt other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMemberExpressionInt)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlComparisonExpression<int> operator ==(DvlSqlMemberExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<int> operator !=(DvlSqlMemberExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<int> operator >(DvlSqlMemberExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<int> operator >=(DvlSqlMemberExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<int> operator <(DvlSqlMemberExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<int> operator <=(DvlSqlMemberExpressionInt lhs,
        DvlSqlConstantExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression<int> operator ==(DvlSqlConstantExpressionInt lhs,
        DvlSqlMemberExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<int> operator !=(DvlSqlConstantExpressionInt lhs,
        DvlSqlMemberExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.NotEquality, rhs);

    public static DvlSqlComparisonExpression<int> operator >(DvlSqlConstantExpressionInt lhs,
        DvlSqlMemberExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<int> operator >=(DvlSqlConstantExpressionInt lhs,
        DvlSqlMemberExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<int> operator <(DvlSqlConstantExpressionInt lhs,
        DvlSqlMemberExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<int> operator <=(DvlSqlConstantExpressionInt lhs,
        DvlSqlMemberExpressionInt rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}

public class DvlSqlMemberExpressionString(string memberName) : DvlSqlMemberExpression<string>(memberName)
{
    protected bool Equals(DvlSqlMemberExpressionString other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMemberExpressionString)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlComparisonExpression<string> operator ==(DvlSqlMemberExpressionString lhs,
        DvlSqlConstantExpressionString rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<string> operator !=(DvlSqlMemberExpressionString lhs,
        DvlSqlConstantExpressionString rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<string> operator ==(DvlSqlConstantExpressionString lhs,
        DvlSqlMemberExpressionString rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<string> operator !=(DvlSqlConstantExpressionString lhs,
        DvlSqlMemberExpressionString rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);
}

public class DvlSqlMemberExpressionDateTime(string memberName) : DvlSqlMemberExpression<DateTime>(memberName)
{
    protected bool Equals(DvlSqlMemberExpressionDateTime other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMemberExpressionDateTime)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlComparisonExpression<DateTime> operator ==(DvlSqlMemberExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<DateTime> operator !=(DvlSqlMemberExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<DateTime> operator >(DvlSqlMemberExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<DateTime> operator >=(DvlSqlMemberExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<DateTime> operator <(DvlSqlMemberExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<DateTime> operator <=(DvlSqlMemberExpressionDateTime lhs,
        DvlSqlConstantExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression<DateTime> operator ==(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMemberExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<DateTime> operator !=(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMemberExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<DateTime> operator >(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMemberExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<DateTime> operator >=(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMemberExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<DateTime> operator <(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMemberExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<DateTime> operator <=(DvlSqlConstantExpressionDateTime lhs,
        DvlSqlMemberExpressionDateTime rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}

public class DvlSqlMemberExpressionDouble(string memberName) : DvlSqlMemberExpression<double>(memberName)
{
    protected bool Equals(DvlSqlMemberExpressionDouble other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMemberExpressionDouble)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlComparisonExpression<double> operator ==(DvlSqlMemberExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<double> operator !=(DvlSqlMemberExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<double> operator >(DvlSqlMemberExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<double> operator >=(DvlSqlMemberExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<double> operator <(DvlSqlMemberExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<double> operator <=(DvlSqlMemberExpressionDouble lhs,
        DvlSqlConstantExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression<double> operator ==(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMemberExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<double> operator !=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMemberExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<double> operator >(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMemberExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<double> operator >=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMemberExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<double> operator <(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMemberExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<double> operator <=(DvlSqlConstantExpressionDouble lhs,
        DvlSqlMemberExpressionDouble rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}

public class DvlSqlMemberExpressionDecimal(string memberName) : DvlSqlMemberExpression<decimal>(memberName)
{
    protected bool Equals(DvlSqlMemberExpressionDecimal other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMemberExpressionDecimal)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlComparisonExpression<decimal> operator ==(DvlSqlMemberExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<decimal> operator !=(DvlSqlMemberExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<decimal> operator >(DvlSqlMemberExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<decimal> operator >=(DvlSqlMemberExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<decimal> operator <(DvlSqlMemberExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<decimal> operator <=(DvlSqlMemberExpressionDecimal lhs,
        DvlSqlConstantExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression<decimal> operator ==(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMemberExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<decimal> operator !=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMemberExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<decimal> operator >(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMemberExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<decimal> operator >=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMemberExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<decimal> operator <(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMemberExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<decimal> operator <=(DvlSqlConstantExpressionDecimal lhs,
        DvlSqlMemberExpressionDecimal rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}

public class DvlSqlMemberExpressionBool(string memberName) : DvlSqlMemberExpression<bool>(memberName)
{
    protected bool Equals(DvlSqlMemberExpressionBool other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMemberExpressionBool)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlComparisonExpression<bool> operator ==(DvlSqlMemberExpressionBool lhs,
        DvlSqlConstantExpressionBool rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<bool> operator !=(DvlSqlMemberExpressionBool lhs,
        DvlSqlConstantExpressionBool rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<bool> operator ==(DvlSqlConstantExpressionBool lhs,
        DvlSqlMemberExpressionBool rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<bool> operator !=(DvlSqlConstantExpressionBool lhs,
        DvlSqlMemberExpressionBool rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);
}

public class DvlSqlMemberExpressionByte(string memberName) : DvlSqlMemberExpression<byte>(memberName)
{
    protected bool Equals(DvlSqlMemberExpressionByte other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMemberExpressionByte)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlComparisonExpression<byte> operator ==(DvlSqlMemberExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<byte> operator !=(DvlSqlMemberExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<byte> operator >(DvlSqlMemberExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<byte> operator >=(DvlSqlMemberExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<byte> operator <(DvlSqlMemberExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<byte> operator <=(DvlSqlMemberExpressionByte lhs,
        DvlSqlConstantExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression<byte> operator ==(DvlSqlConstantExpressionByte lhs,
        DvlSqlMemberExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<byte> operator !=(DvlSqlConstantExpressionByte lhs,
        DvlSqlMemberExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<byte> operator >(DvlSqlConstantExpressionByte lhs,
        DvlSqlMemberExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<byte> operator >=(DvlSqlConstantExpressionByte lhs,
        DvlSqlMemberExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<byte> operator <(DvlSqlConstantExpressionByte lhs,
        DvlSqlMemberExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<byte> operator <=(DvlSqlConstantExpressionByte lhs,
        DvlSqlMemberExpressionByte rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}

public class DvlSqlMemberExpressionLong(string memberName) : DvlSqlMemberExpression<long>(memberName)
{
    protected bool Equals(DvlSqlMemberExpressionLong other) => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DvlSqlMemberExpressionLong)obj);
    }

    public override int GetHashCode() => throw new NotImplementedException();

    public static DvlSqlComparisonExpression<long> operator ==(DvlSqlMemberExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<long> operator !=(DvlSqlMemberExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<long> operator >(DvlSqlMemberExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<long> operator >=(DvlSqlMemberExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<long> operator <(DvlSqlMemberExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<long> operator <=(DvlSqlMemberExpressionLong lhs,
        DvlSqlConstantExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression<long> operator ==(DvlSqlConstantExpressionLong lhs,
        DvlSqlMemberExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<long> operator !=(DvlSqlConstantExpressionLong lhs,
        DvlSqlMemberExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<long> operator >(DvlSqlConstantExpressionLong lhs,
        DvlSqlMemberExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<long> operator >=(DvlSqlConstantExpressionLong lhs,
        DvlSqlMemberExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<long> operator <(DvlSqlConstantExpressionLong lhs,
        DvlSqlMemberExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<long> operator <=(DvlSqlConstantExpressionLong lhs,
        DvlSqlMemberExpressionLong rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}