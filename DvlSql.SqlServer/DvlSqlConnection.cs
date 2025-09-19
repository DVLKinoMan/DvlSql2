using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace DvlSql.SqlServer;

internal class DvlSqlConnection : IDvlSqlConnection
{
    // private readonly List<SqlCommand> _commands = new List<SqlCommand>();
    private readonly SqlConnection _connection;
    private DbTransaction? _transaction;
    private readonly IDvlSqlMsCommandFactory _commandFactory;

    public DvlSqlConnection(string connectionString) =>
        (_connection, _commandFactory) = (new(connectionString), new DvlSqlMsCommandFactory());
    
    public DvlSqlConnection(string connectionString, IDvlSqlMsCommandFactory commandFactory) =>
        (_connection, _commandFactory) = (new(connectionString), commandFactory);

    public void Dispose()
    {
        //this._commands.Clear();
        _transaction?.Dispose();
        _transaction = null;
        if(_connection.State != ConnectionState.Closed)
            _connection.Close();
    }

    public async Task<TResult> ConnectAsync<TResult>(Func<IDvlSqlCommand, Task<TResult>> func, string sqlString,
        CommandType commandType = CommandType.Text, params DvlSqlParameter[]? parameters)
    {
        if (_transaction == null)
            await _connection.OpenAsync();
        using var command =
            _commandFactory.CreateSqlCommand(commandType, _connection, sqlString, _transaction, 
            parameters?.ToSqlParameters().ToArray());
        try
        {
            return await func(command);
        }
        finally
        {
            if (_transaction == null)
                await _connection.CloseAsync();
        }
    }

    public async ValueTask<DbTransaction> BeginTransactionAsync(CancellationToken token = default)
    {
        await _connection.OpenAsync(token);
        return _transaction = await _connection.BeginTransactionAsync(token);
    }

    public async Task CommitAsync(CancellationToken token = default)
    {
        if (_transaction == null)
            throw new InvalidOperationException($"{nameof(_transaction)} is null");
     
        await _transaction.CommitAsync(token);
    }

    public async Task RollbackAsync(CancellationToken token = default) 
    {
        if (_transaction == null)
            throw new InvalidOperationException($"{nameof(_transaction)} is null");

        await _transaction.RollbackAsync(token);
    }

    public IDvlSqlConnection GetClone() =>
        new DvlSqlConnection(_connection.ConnectionString, _commandFactory);

    public async ValueTask DisposeAsync()
    {
        //this._commands.Clear();
        if (_transaction != null)
            await _transaction.DisposeAsync();
        _transaction = null;
        if (_connection.State != ConnectionState.Closed)
            await _connection.CloseAsync();
    }
}
