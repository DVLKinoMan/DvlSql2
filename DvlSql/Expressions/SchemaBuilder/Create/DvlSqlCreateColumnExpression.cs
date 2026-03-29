using System.Data;

namespace DvlSql.Expressions;

public class DvlSqlCreateColumnExpression(string name) : DvlSqlColumnExpression(name)
{
    public void Accept(ICreateTableVisitor visitor) => visitor.Visit(this);
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
}