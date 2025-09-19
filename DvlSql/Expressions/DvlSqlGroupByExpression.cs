namespace DvlSql.Expressions;

public class DvlSqlGroupByExpression : DvlSqlExpressionWithParameters
{
    public List<string> ParameterNames;
    public DvlSqlGroupByBinaryExpression? BinaryExpression { get; set; }

    public DvlSqlGroupByExpression(IEnumerable<string> parameterNames) =>
        (ParameterNames, IsRoot) = (parameterNames.ToList(), true);

    // public DvlSqlGroupByExpression WithRoot(bool isRoot)
    // {
    //     this.IsRoot = isRoot;
    //     return this;
    // }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => GroupByClone();

    public DvlSqlGroupByExpression GroupByClone() => new([.. ParameterNames]);
}

public class DvlSqlGroupByBinaryEmptyExpression : DvlSqlGroupByBinaryExpression
{
    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => this;

    public override DvlSqlGroupByBinaryEmptyExpression BinaryClone() => this;

    public override void NotOnThis()
    {
        Not = !Not;
    }
}