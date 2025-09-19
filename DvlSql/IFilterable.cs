using DvlSql.Expressions;

namespace DvlSql;

public interface IFilterable
{
    IFilter Where(DvlSqlBinaryExpression binaryExpression);
    IFilter Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
}
