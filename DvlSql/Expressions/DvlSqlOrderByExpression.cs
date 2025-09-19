namespace DvlSql.Expressions;

public enum Ordering
{
    ASC,
    DESC
}

public class DvlSqlOrderByExpression : DvlSqlExpression
{
    public List<(string column, Ordering ordering)> Params { get; set; }

    public DvlSqlOrderByExpression(IEnumerable<(string column, Ordering ordering)> @params) =>
        (Params, IsRoot) = (@params.ToList(), true);

    public DvlSqlOrderByExpression(params (string column, Ordering ordering)[] @params) =>
        (Params, IsRoot) = (@params.ToList(), true);

    public void Add((string column, Ordering ordering) param) => Params.Add(param);

    public void AddRange(IEnumerable<(string column, Ordering ordering)> @params) => Params.AddRange(@params);

    // public DvlSqlOrderByExpression WithRoot(bool isRoot)
    // {
    //     this.IsRoot = isRoot;
    //     return this;
    // }

    public void ReverseOrdering()
    {
        Params[0] = (Params[0].column, Params[0].ordering == Ordering.DESC ? Ordering.ASC : Ordering.DESC);
    }

    public override void Accept(ISqlExpressionVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override DvlSqlExpression Clone() => OrderByClone();

    public DvlSqlOrderByExpression OrderByClone() => new([.. Params]);
}