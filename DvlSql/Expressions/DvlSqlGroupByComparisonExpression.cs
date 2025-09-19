namespace DvlSql.Expressions;

public class DvlSqlGroupByComparisonExpression<T> : DvlSqlGroupByBinaryExpression
{
    public DvlSqlGroupByComparisonExpression(
        DvlSqlComparableExpression<T> leftExpression,
        SqlComparisonOperator comparisonOperator,
        DvlSqlComparableExpression<T> rightExpression)
    {
        LeftExpression = leftExpression;
        ComparisonOperator = comparisonOperator;
        RightExpression = rightExpression;
        if (leftExpression.Parameter != null)
            Parameters.Add(leftExpression.Parameter);
        if (rightExpression.Parameter == null)
            return;
        Parameters.Add(rightExpression.Parameter);
    }

    public DvlSqlComparableExpression<T> LeftExpression { get; }

    public SqlComparisonOperator ComparisonOperator { get; }

    public DvlSqlComparableExpression<T> RightExpression { get; }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => BinaryClone();

    public override DvlSqlGroupByBinaryExpression BinaryClone() =>
        new DvlSqlGroupByComparisonExpression<T>(LeftExpression.ComparableClone(), ComparisonOperator,
                RightExpression.ComparableClone())
            .SetNot(Not);

    public override void NotOnThis()
    {
        Not = !Not;
    }
    
    public static implicit operator DvlSqlGroupByComparisonExpression<T>(DvlSqlComparisonExpression<T> expression) =>
        new(expression.LeftExpression, expression.ComparisonOperator, expression.RightExpression);
}

public class DvlSqlGroupByComparisonExpression : DvlSqlGroupByBinaryExpression
{
    public DvlSqlGroupByComparisonExpression(
        DvlSqlComparableExpression leftExpression,
        SqlComparisonOperator comparisonOperator,
        DvlSqlComparableExpression rightExpression)
    {
        LeftExpression = leftExpression;
        ComparisonOperator = comparisonOperator;
        RightExpression = rightExpression;
        if (leftExpression.Parameter != null)
            Parameters.Add(leftExpression.Parameter);
        if (rightExpression.Parameter == null)
            return;
        Parameters.Add(rightExpression.Parameter);
    }

    public DvlSqlComparableExpression LeftExpression { get; }

    public SqlComparisonOperator ComparisonOperator { get; }

    public DvlSqlComparableExpression RightExpression { get; }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => BinaryClone();

    public override DvlSqlGroupByBinaryExpression BinaryClone() =>
        new DvlSqlGroupByComparisonExpression(LeftExpression.BaseComparableClone(), ComparisonOperator,
                RightExpression.BaseComparableClone())
            .SetNot(Not);

    public override void NotOnThis()
    {
        Not = !Not;
    }

    public static implicit operator DvlSqlGroupByComparisonExpression(DvlSqlComparisonExpression expression) =>
        new(expression.LeftExpression, expression.ComparisonOperator, expression.RightExpression);
}