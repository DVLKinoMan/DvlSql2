using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace DvlSql.SqlServer;

internal class DvlSqlConnection : IDvlSqlConnection
{
    // private readonly List<SqlCommand> _commands = new List<SqlCommand>();
    private readonly SqlConnection _connection;
    private readonly DvlSqlOptions _options;
    private DbTransaction? _transaction;
    private readonly IDvlSqlMsCommandFactory _commandFactory;
    private readonly ILogger? _logger;

    public DvlSqlConnection(DvlSqlOptions options, ILogger? logger)
    {
        _connection = new(options.ConnectionString);
        _commandFactory = new DvlSqlMsCommandFactory();
        _options = options;
        _logger = logger;
    }

    public DvlSqlConnection(DvlSqlOptions options, IDvlSqlMsCommandFactory commandFactory, ILogger? logger)
    {
        _connection = new(options.ConnectionString);
        _commandFactory = commandFactory;
        _options = options;
        _logger = logger;
    }

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
            Log(command);
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
        new DvlSqlConnection(_options, _commandFactory, _logger);

    public async ValueTask DisposeAsync()
    {
        //this._commands.Clear();
        if (_transaction != null)
            await _transaction.DisposeAsync();
        _transaction = null;
        if (_connection.State != ConnectionState.Closed)
            await _connection.CloseAsync();
    }
    
    private void Log(IDvlSqlCommand command)
    {
        _logger?.LogDebug("Executing: {SQL}", command.CommandText);
    }
}
