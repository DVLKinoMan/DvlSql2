namespace DvlSql.Expressions;

public abstract class DvlSqlGroupBySelectableExpression<TValue>(string selectedName)
    : DvlSqlComparableExpression<TValue>
{
    public string MemberName { get; init; } = selectedName;
}