namespace DvlSql.Expressions;

public class DvlSqlLikeExpression(string field, string pattern) : DvlSqlBinaryExpression
{
    public string Field { get; init; } = field;
    public string Pattern { get; init; } = pattern;

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => BinaryClone();

    public override DvlSqlBinaryExpression BinaryClone() => new DvlSqlLikeExpression(Field, Pattern).SetNot(Not);

    public override void NotOnThis()
    {
        Not = !Not;
    }
}