using System.Data;
using static System.Exts.Extensions;

namespace DvlSql;

public class DvlSqlType
{
    public string? Name { get; }

    public SqlDbType SqlDbType { get; init; }

    public int? Size { get; init; }

    public bool IsNotNull { get; init; } = false;

    public byte? Precision { get; init; }

    public byte? Scale { get; init; }

    public DvlSqlType(string? name, SqlDbType dbType, int? size = null, bool? isNotNull = null, byte? precision = null, byte? scale = null)
    {
        Name = name;
        Size = size;
        Precision = precision;
        Scale = scale;
        SqlDbType = dbType;
        IsNotNull = isNotNull ?? IsNotNull;
    }

    public DvlSqlType(SqlDbType dbType, int? size = null, bool? isNotNull = null, byte? precision = null, byte? scale = null)
    {
        Size = size;
        Precision = precision;
        Scale = scale;
        SqlDbType = dbType;
        IsNotNull = isNotNull ?? IsNotNull;
    }

    
    public override bool Equals(object obj) => 
        obj is DvlSqlType type && Equals(type);

    private bool Equals(DvlSqlType other) =>
        other.Name == Name &&
        other.Precision == Precision &&
        other.Scale == Scale &&
        other.Size == Size &&
        other.SqlDbType == SqlDbType;

    public override int GetHashCode() => (Name == null ? 0 : Name.GetHashCode()) +
                                         Precision.GetHashCode() +
                                         Scale.GetHashCode() +
                                         Size.GetHashCode() +
                                         SqlDbType.GetHashCode();
}

public sealed class DvlSqlType<TValue> : DvlSqlType
{
    public TValue Value { get; init; }
    public bool ExactValue { get; init; }

    public DvlSqlType(TValue value, DvlSqlType dvlSqlType, bool exactValue = false) : base(dvlSqlType.Name, dvlSqlType.SqlDbType,
        dvlSqlType.Size, dvlSqlType.IsNotNull, dvlSqlType.Precision, dvlSqlType.Scale)
    {
        Value = value;
        ExactValue = exactValue;
    }

    public DvlSqlType(string name, TValue value, int? size = null, bool? isNotNull = null, byte? precision = null, byte? scale = null, bool exactValue = false) :
        base(name, DefaultMap(value), size, isNotNull, precision, scale)
    {
        Value = value;
        ExactValue = exactValue;
    }

    public DvlSqlType(string name, TValue value, SqlDbType dbType, int? size = null, bool? isNotNull = null, byte? precision = null,
        byte? scale = null, bool exactValue = false) : base(name, dbType, size, isNotNull, precision, scale)
    {
        Value = value;
        ExactValue = exactValue;
    }

    public DvlSqlType(TValue value, SqlDbType dbType, int? size = null, bool? isNotNull = null, byte? precision = null,
        byte? scale = null, bool exactValue = false) : base(null, dbType, size, isNotNull, precision, scale)
    {
        Value = value;
        ExactValue = exactValue;
    }

    public override bool Equals(object obj) => 
        obj is DvlSqlType<TValue> type && Equals(type);

    private bool Equals(DvlSqlType<TValue> other) =>
        other.Value!.Equals(Value) &&
        base.Equals(other);

    public override int GetHashCode() => EqualityComparer<TValue>.Default.GetHashCode(Value!) + base.GetHashCode();
}
