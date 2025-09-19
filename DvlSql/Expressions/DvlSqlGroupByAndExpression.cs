namespace DvlSql.Expressions;

public class DvlSqlGroupByAndExpression : DvlSqlGroupByBinaryExpression
{
    public IEnumerable<DvlSqlGroupByBinaryExpression> InnerExpressions { get; }

    public DvlSqlGroupByAndExpression(params DvlSqlGroupByBinaryExpression[] binaryExpressions)
    {
        InnerExpressions = binaryExpressions;
        Parameters = binaryExpressions.SelectMany(b => b.Parameters).ToList();
    }

    public DvlSqlGroupByAndExpression(
        IEnumerable<DvlSqlGroupByBinaryExpression> binaryExpressions)
    {
        var dvlSqlBinaryExpressions = binaryExpressions.ToList();
        InnerExpressions = dvlSqlBinaryExpressions;
        Parameters = dvlSqlBinaryExpressions.SelectMany(b => b.Parameters).ToList();
    }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => (DvlSqlExpression) this.BinaryClone();

    public override DvlSqlGroupByBinaryExpression BinaryClone() =>
        new DvlSqlGroupByAndExpression(InnerExpressions.Select(inner => inner.BinaryClone()))
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