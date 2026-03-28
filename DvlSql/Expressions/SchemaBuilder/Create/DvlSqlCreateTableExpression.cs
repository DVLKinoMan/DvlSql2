namespace DvlSql.Expressions;

public class DvlSqlCreateTableExpression(string name)
{
    public string Name { get; } = name;
    public List<DvlSqlColumnExpression> ColumnExpressions = [];

    public void Accept(ISchemaBuilderVisitor visitor) => visitor.Visit(this);
}