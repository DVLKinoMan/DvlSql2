namespace DvlSql.Expressions;

public class DvlSqlOrExpression : DvlSqlBinaryExpression
{
    public DvlSqlOrExpression(params DvlSqlBinaryExpression[] binaryExpressions)
    {
        InnerExpressions = binaryExpressions;
        Parameters = binaryExpressions.SelectMany(b => b.Parameters).ToList();
    }

    public IEnumerable<DvlSqlBinaryExpression> InnerExpressions { get; init; }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => BinaryClone();

    public override DvlSqlBinaryExpression BinaryClone() =>
        new DvlSqlOrExpression(InnerExpressions.Select(inner => inner.BinaryClone()).ToArray())
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