using System.Data;
using System.Exts;
using System.Text;
using DvlSql.Expressions;

namespace DvlSql.PostgreSql;

internal class SqlUpdateable(IDvlSqlConnection dvlSqlConnection, DvlSqlUpdateExpression updateExpression) : IUpdateable
{
    private readonly IDvlSqlConnection _dvlSqlConnection = dvlSqlConnection;
    private readonly DvlSqlUpdateExpression _updateExpression = updateExpression;
    private IInsertDeleteExecutable<int>? _updateExecutable;

    public IUpdateable Set<TVal>(DvlSqlType<TVal> value)
    {
        _updateExpression.Add(value);

        return this;
    }

    public IUpdateable Set(DvlSqlParameter value)
    {
        _updateExpression.Add(value);

        return this;
    }

    public async Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default) =>
        _updateExecutable != null
            ? await _updateExecutable.ExecuteAsync(timeout, cancellationToken)
            : await CreateDeleteExecutable().ExecuteAsync(timeout, cancellationToken);

    public Task<TResult> ExecuteAsync<TResult>(Func<IDataReader, TResult> reader, int? timeout = default,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IInsertDeleteExecutable<int> Where(DvlSqlBinaryExpression binaryExpression, params DvlSqlParameter[] @params)
    {
        _updateExpression.WhereExpression =
            new DvlSqlWhereExpression(binaryExpression).WithParameters(@params
                .Select(p => p.WithName(p.Name.WithAlpha())).ToList()) as DvlSqlWhereExpression;

        return _updateExecutable = CreateDeleteExecutable();
    }

    public IInsertDeleteExecutable<int> CreateDeleteExecutable() =>
        new SqlInsertDeleteExecutable<int>(_dvlSqlConnection, ToString, GetDvlSqlParameters,
            (command, timeout, token) => command.ExecuteNonQueryAsync(timeout, token ?? default));

    public override string ToString()
    {
        var builder = new StringBuilder();
        var commandBuilder = new DvlSqlCommandBuilder(builder);

        _updateExpression.Accept(commandBuilder);

        string result = builder.ToString();

        Statics.ParametersCount = 0;

        return result;
    }

    private IEnumerable<DvlSqlParameter> GetDvlSqlParameters() =>
        _updateExpression.DvlSqlParameters.Union(_updateExpression.WhereExpression?.Parameters ??
                                                 Enumerable.Empty<DvlSqlParameter>());
}