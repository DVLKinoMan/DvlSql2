namespace DvlSql.Expressions;

public class DvlSqlExistsExpression : DvlSqlBinaryExpression
{
    public DvlSqlExistsExpression(DvlSqlFullSelectExpression select)
    {
        Select = select;
        Parameters = select.Where?.Parameters ?? [];
    }

    public DvlSqlFullSelectExpression Select { get; init; }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => BinaryClone();

    public override DvlSqlBinaryExpression BinaryClone() =>
        new DvlSqlExistsExpression(Select.FullSelectClone())
            .SetNot(Not);

    public override void NotOnThis()
    {
        Not = !Not;
    }
}