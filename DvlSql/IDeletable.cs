using System.Data;
using DvlSql.Expressions;

namespace DvlSql;

public interface IDeletable : IDeleteJoinable
{
    IDeleteOutputable<TResult> Output<TResult>(Func<IDataReader, TResult> reader, params string[] cols);
}

public interface IDeleteOutputable<TResult>
{
    IInsertDeleteExecutable<TResult> Where(DvlSqlBinaryExpression binaryExpression);
    IInsertDeleteExecutable<TResult> Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
}
