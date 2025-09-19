namespace DvlSql.Expressions;

public class DvlSqlGroupByOrExpression : DvlSqlGroupByBinaryExpression
{
    public DvlSqlGroupByOrExpression(params DvlSqlGroupByBinaryExpression[] binaryExpressions)
    {
        InnerExpressions = binaryExpressions;
        Parameters = binaryExpressions.SelectMany(b => b.Parameters).ToList();
    }

    public IEnumerable<DvlSqlGroupByBinaryExpression> InnerExpressions { get; init; }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => BinaryClone();

    public override DvlSqlGroupByBinaryExpression BinaryClone() =>
        new DvlSqlGroupByOrExpression(InnerExpressions.Select(inner => inner.BinaryClone()).ToArray())
            .SetNot(Not);

    public override void NotOnThis()
    {
        Not = !Not;
        foreach (var inner in InnerExpressions)
        {
            var something = !inner;
        }
    }
}