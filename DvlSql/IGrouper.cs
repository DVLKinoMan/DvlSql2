using DvlSql.Expressions;

namespace DvlSql;

public interface IGrouper : ISelectable
{
    ISelectable Having(DvlSqlGroupByBinaryExpression binaryExpression);
    ISelectable Having(DvlSqlGroupByBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
}