namespace DvlSql.Expressions;

public class DvlSqlAlterColumnExpression(string name) : DvlSqlColumnExpression(name)
{
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
}