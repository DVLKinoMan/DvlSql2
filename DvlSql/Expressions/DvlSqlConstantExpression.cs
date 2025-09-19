namespace DvlSql.Expressions;

public class DvlSqlConstantExpression<TValue>(TValue value, string? name = null)
    : DvlSqlComparableExpression<TValue>(value, name)
{
    protected bool Equals(DvlSqlConstantExpression<TValue> other) =>
        EqualityComparer<TValue>.Default.Equals(Value, other.Value);

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((DvlSqlConstantExpression<TValue>)obj);
    }

    public override string ToString() => Parameter!.Name;

    public override int GetHashCode() => EqualityComparer<TValue>.Default.GetHashCode(Value!);

    public override DvlSqlComparableExpression<TValue> ComparableClone() =>
        new DvlSqlConstantExpression<TValue>(Value, Parameter?.Name);

    private TValue Value { get; } = value;

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => ComparableClone();

    public override DvlSqlComparableExpression BaseComparableClone() => ComparableClone();
}

public class DvlSqlConstantExpressionInt(int value, string? name = null) : DvlSqlConstantExpression<int>(value, name)
{
    public static implicit operator DvlSqlConstantExpressionInt(int num) => new(num);
}

public class DvlSqlConstantExpressionDouble(double value, string? name = null)
    : DvlSqlConstantExpression<double>(value, name)
{
    public static implicit operator DvlSqlConstantExpressionDouble(double num) => new(num);
}

public class DvlSqlConstantExpressionString(string value, string? name = null)
    : DvlSqlConstantExpression<string>(value, name)
{
    public static implicit operator DvlSqlConstantExpressionString(string val) => new(val);
}

public class DvlSqlConstantExpressionDecimal(decimal value, string? name = null)
    : DvlSqlConstantExpression<decimal>(value, name)
{
    public static implicit operator DvlSqlConstantExpressionDecimal(decimal val) => new(val);
}

public class DvlSqlConstantExpressionDateTime(DateTime value, string? name = null)
    : DvlSqlConstantExpression<DateTime>(value, name)
{
    public static implicit operator DvlSqlConstantExpressionDateTime(DateTime val) => new(val);
}

public class DvlSqlConstantExpressionBool(bool value, string? name = null)
    : DvlSqlConstantExpression<bool>(value, name)
{
    public static implicit operator DvlSqlConstantExpressionBool(bool val) => new(val);
}

public class DvlSqlConstantExpressionByte(byte value, string? name = null)
    : DvlSqlConstantExpression<byte>(value, name)
{
    public static implicit operator DvlSqlConstantExpressionByte(byte val) => new(val);
}

public class DvlSqlConstantExpressionLong(long value, string? name = null)
    : DvlSqlConstantExpression<long>(value, name)
{
    public static implicit operator DvlSqlConstantExpressionLong(long val) => new(val);
}