namespace DvlSql.Expressions;

public class DvlSqlCreateTableExpression(string name)
{
    public string Name { get; } = name;
    public List<DvlSqlCreateColumnExpression> ColumnExpressions = [];

    public void Accept(ICreateTableVisitor visitor) => visitor.Visit(this);
}