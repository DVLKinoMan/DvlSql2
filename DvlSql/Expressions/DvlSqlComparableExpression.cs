namespace DvlSql.Expressions;

public abstract class DvlSqlComparableExpression : DvlSqlExpression
{
    public abstract DvlSqlParameter? Parameter { get; }

    public static implicit operator DvlSqlComparableExpression(string str) => new DvlSqlConstantExpression<string>(str);

    public static implicit operator DvlSqlComparableExpression(int num) => new DvlSqlConstantExpression<int>(num);
    
    public static implicit operator DvlSqlComparableExpression(Guid id) => new DvlSqlConstantExpression<Guid>(id);

    public static implicit operator DvlSqlComparableExpression(long num) => new DvlSqlConstantExpression<long>(num);

    public static implicit operator DvlSqlComparableExpression(double num) => new DvlSqlConstantExpression<double>(num);

    public static implicit operator DvlSqlComparableExpression(decimal num) =>
        new DvlSqlConstantExpression<decimal>(num);

    public static implicit operator DvlSqlComparableExpression(DateTime dateTime) =>
        new DvlSqlConstantExpression<DateTime>(dateTime);

    public static implicit operator DvlSqlComparableExpression(bool num) =>
        new DvlSqlConstantExpression<bool>(num);

    public static implicit operator DvlSqlComparableExpression(byte num) =>
        new DvlSqlConstantExpression<byte>(num);

    public abstract DvlSqlComparableExpression BaseComparableClone();
}

public abstract class DvlSqlComparableExpression<T> : DvlSqlComparableExpression
{
    protected DvlSqlComparableExpression(T value, string? name = null) => Parameter =
        new DvlSqlParameter<T>(GetName(name: name), new DvlSqlType<T>(GetName(true, name), value));

    protected DvlSqlComparableExpression()
    {
    }

    // private static int _count = 0;
    private static string GetName(bool theSame = false, string? name = null) =>
        name ?? ($"p{(theSame ? Statics.ParametersCount : ++Statics.ParametersCount)}");

    public override DvlSqlParameter? Parameter { get; }

    public abstract DvlSqlComparableExpression<T> ComparableClone();
}