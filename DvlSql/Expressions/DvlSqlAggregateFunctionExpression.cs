namespace DvlSql.Expressions;

public abstract class DvlSqlAggregateFunctionExpression<TResult>(string memberName)
    : DvlSqlGroupBySelectableExpression<TResult>(memberName)
{
}