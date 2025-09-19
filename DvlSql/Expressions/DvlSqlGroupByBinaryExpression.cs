namespace DvlSql.Expressions;

public abstract class DvlSqlGroupByBinaryExpression : DvlSqlExpressionWithParameters
{
    public bool Not { get; set; }
    
    public static DvlSqlGroupByAndExpression operator &(DvlSqlGroupByBinaryExpression leftBinaryExpression,
        DvlSqlGroupByBinaryExpression rightBinaryExpression) =>
        new(leftBinaryExpression, rightBinaryExpression);

    public static DvlSqlGroupByOrExpression operator |(DvlSqlGroupByBinaryExpression leftBinaryExpression,
        DvlSqlGroupByBinaryExpression rightBinaryExpression) =>
        new(leftBinaryExpression, rightBinaryExpression);

    public static DvlSqlGroupByBinaryExpression operator !(DvlSqlGroupByBinaryExpression binaryExpression)
    {
        binaryExpression.NotOnThis();
        return binaryExpression;
    }
    
    public abstract DvlSqlGroupByBinaryExpression BinaryClone();

    public abstract void NotOnThis();
    
    public static implicit operator DvlSqlGroupByBinaryExpression(DvlSqlComparisonExpression expression) =>
        new DvlSqlGroupByComparisonExpression(expression.LeftExpression, expression.ComparisonOperator, expression.RightExpression);
}