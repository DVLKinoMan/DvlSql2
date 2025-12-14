using System.Data;
using Microsoft.Data.SqlClient;

namespace DvlSql.SqlServer;

/// <summary>
/// todo maybe sqlcommand will have timeout and cancellationtoken already
/// </summary>
internal class DvlSqlCommand(SqlCommand command) : IDvlSqlCommand //: IDvlSqlCommand<TResult>
{
    private readonly SqlCommand _sqlCommand = command;

    private static SqlCommand WithTimeout(SqlCommand command, int timeout)
    {
        command.CommandTimeout = timeout;
        return command;
    }

    public async Task<int> ExecuteNonQueryAsync(int? timeout = default, CancellationToken cancellationToken = default)
    {
        if (timeout != null)
            WithTimeout(_sqlCommand, (int) timeout);

        return await _sqlCommand.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<TResult> ExecuteReaderAsync<TResult>(Func<IDataReader, TResult> converterFunc,
        int? timeout = default,
        CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
    {
        if (timeout != null)
            WithTimeout(_sqlCommand, (int) timeout);

        await using var reader = await _sqlCommand.ExecuteReaderAsync(behavior, cancellationToken);

        return converterFunc(reader);
    }

    public async Task<TResult> ExecuteScalarAsync<TResult>(Func<object, TResult> converterFunc, 
        int? timeout = default, CancellationToken cancellationToken = default)
    {
        if (timeout != null)
            WithTimeout(_sqlCommand, (int)timeout);

        var result = await _sqlCommand.ExecuteScalarAsync(cancellationToken);

        return converterFunc(result);
    }

    public string CommandText => _sqlCommand.CommandText;

    public void Dispose()
    {
        _sqlCommand?.Dispose();
        _sqlCommand?.Parameters.Clear();
    }
}
