namespace DvlSql.Expressions;

public abstract class DvlSqlSchemaExpression
{
    public abstract void Visit(ICodeBuilder visitor);
}