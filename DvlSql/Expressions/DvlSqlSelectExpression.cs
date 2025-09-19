namespace DvlSql.Expressions;

public class DvlSqlSelectExpression : DvlSqlExpression
{
    public int? Top { get; set; }
    public List<string> ParameterNames { get; init; } = [];

    //public DvlSqlFromExpression From { get; }
    // public new bool IsRoot { get; private set; } = true;

    public DvlSqlSelectExpression(int? top = null) =>
        Top = top;

    public DvlSqlSelectExpression(List<string> parameterNames,
        int? top = null) =>
        (ParameterNames, Top) = (parameterNames, top);

    public DvlSqlSelectExpression(params string[] parameterNames) =>
        (ParameterNames) = [.. parameterNames];

    // public DvlSqlSelectExpression WithRoot(bool isRoot)
    // {
    //     this.IsRoot = isRoot;
    //     return this;
    // }

    public void Add(string paramName) => ParameterNames.Add(paramName);

    public void AddRange(IEnumerable<string> paramNames)
    {
        foreach (var paramName in paramNames)
            ParameterNames.Add(paramName);
    }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => SelectClone();

    public DvlSqlSelectExpression SelectClone() => new(ParameterNames, Top);
}