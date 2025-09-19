namespace DvlSql.Expressions;

public class DvlSqlComparisonExpression<T> : DvlSqlBinaryExpression
{
    public DvlSqlComparisonExpression(DvlSqlComparableExpression<T> leftExpression,
        SqlComparisonOperator comparisonOperator,
        DvlSqlComparableExpression<T> rightExpression)
    {
        LeftExpression = leftExpression;
        ComparisonOperator = comparisonOperator;
        RightExpression = rightExpression;
        if (leftExpression.Parameter != null)
            Parameters.Add(leftExpression.Parameter);
        if (rightExpression.Parameter != null)
            Parameters.Add(rightExpression.Parameter);
    }

    public DvlSqlComparableExpression<T> LeftExpression { get; }
    public SqlComparisonOperator ComparisonOperator { get; }
    public DvlSqlComparableExpression<T> RightExpression { get; }

    public override DvlSqlExpression Clone() => BinaryClone();

    public override DvlSqlBinaryExpression BinaryClone() =>
        new DvlSqlComparisonExpression<T>(LeftExpression.ComparableClone(), ComparisonOperator,
                RightExpression.ComparableClone())
            .SetNot(Not);

    public override void NotOnThis()
    {
        Not = !Not;
    }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
}

public class DvlSqlComparisonExpression : DvlSqlBinaryExpression
{
    public DvlSqlComparisonExpression(DvlSqlComparableExpression leftExpression,
        SqlComparisonOperator comparisonOperator,
        DvlSqlComparableExpression rightExpression)
    {
        LeftExpression = leftExpression;
        ComparisonOperator = comparisonOperator;
        RightExpression = rightExpression;
        if (leftExpression.Parameter != null)
            Parameters.Add(leftExpression.Parameter);
        if (rightExpression.Parameter != null)
            Parameters.Add(rightExpression.Parameter);
    }

    public DvlSqlComparableExpression LeftExpression { get; }
    public SqlComparisonOperator ComparisonOperator { get; }
    public DvlSqlComparableExpression RightExpression { get; }

    public override DvlSqlExpression Clone() => BinaryClone();

    public override DvlSqlBinaryExpression BinaryClone() =>
        new DvlSqlComparisonExpression(LeftExpression.BaseComparableClone(), ComparisonOperator,
                RightExpression.BaseComparableClone())
            .SetNot(Not);

    public override void NotOnThis()
    {
        Not = !Not;
    }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
}