namespace DvlSql.Expressions;

public class DvlSqlAndExpression : DvlSqlBinaryExpression
{
    public IEnumerable<DvlSqlBinaryExpression> InnerExpressions { get; }

    //todo: params with .net 9
    public DvlSqlAndExpression(params DvlSqlBinaryExpression[] binaryExpressions)
    {
        InnerExpressions = binaryExpressions;
        Parameters = binaryExpressions.SelectMany(b => b.Parameters).ToList();
    }

    public DvlSqlAndExpression(IEnumerable<DvlSqlBinaryExpression> binaryExpressions)
    {
        var dvlSqlBinaryExpressions = binaryExpressions.ToList();
        InnerExpressions = dvlSqlBinaryExpressions;
        Parameters = dvlSqlBinaryExpressions.SelectMany(b => b.Parameters).ToList();
    }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => BinaryClone();

    public override DvlSqlBinaryExpression BinaryClone() =>
        new DvlSqlAndExpression(InnerExpressions.Select(inner => inner.BinaryClone()))
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