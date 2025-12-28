namespace DvlSql.Expressions;

public class DvlSqlUpdateExpression(DvlSqlFromWithTableExpression fromExpression) : DvlSqlExpressionWithParameters
{
    public DvlSqlFromWithTableExpression FromExpression { get; init; } = fromExpression;
    public DvlSqlWhereExpression? WhereExpression { get; set; }
    public List<DvlSqlParameter> DvlSqlParameters { get; set; } = [];

    public void Add<TVal>(DvlSqlType<TVal> val) =>
        DvlSqlParameters.Add(new DvlSqlParameter<TVal>(val.Name ?? throw new ArgumentNullException(nameof(val)),
            val));

    public void Add(DvlSqlParameter val) =>
        DvlSqlParameters.Add(val);

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => UpdateClone();

    public DvlSqlUpdateExpression UpdateClone() => new(new(FromExpression.TableName, FromExpression.WithNoLock))
    {
        WhereExpression = WhereExpression?.WhereClone(),
        DvlSqlParameters = [.. DvlSqlParameters]
    };
}