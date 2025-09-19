namespace DvlSql.Expressions;

public class DvlSqlInExpression : DvlSqlBinaryExpression
{
    public string ParameterName { get; init; }
    public IEnumerable<DvlSqlExpression> InnerExpressions { get; init; }

    public DvlSqlInExpression(string parameterName, params DvlSqlExpression[] innerExpressions)
    {
        ParameterName = parameterName;
        InnerExpressions = innerExpressions;
        Parameters = GetParameters(innerExpressions);
    }

    private static List<DvlSqlParameter> GetParameters(DvlSqlExpression[] innerExpressions)
    {
        List<DvlSqlParameter> list = [];
        foreach (var exp in innerExpressions)
            if (exp is DvlSqlExpressionWithParameters withParameters)
                list.AddRange(withParameters.Parameters);
            else if (exp is DvlSqlComparableExpression { Parameter: not null } comparableExpression)
                list.Add(comparableExpression.Parameter);

        return list;
    }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => BinaryClone();

    public override DvlSqlBinaryExpression BinaryClone() =>
        new DvlSqlInExpression(ParameterName, InnerExpressions.Select(inner => inner.Clone()).ToArray())
            .SetNot(Not);

    public override void NotOnThis()
    {
        Not = !Not;
    }
}