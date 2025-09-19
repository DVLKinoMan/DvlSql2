using System.Data;

namespace DvlSql.SqlServer;

internal class SqlProcedureExecutable (IDvlSqlConnection dvlSqlConnection, string procedureName,
        params DvlSqlParameter[] parameters) : IProcedureExecutable
{
    private readonly IDvlSqlConnection _dvlSqlConnection = dvlSqlConnection;
    private readonly string _procedureName = procedureName;
    private readonly DvlSqlParameter[] _parameters = parameters;

    public async Task<int> ExecuteAsync(int? timeout = default,
        CommandBehavior behavior = CommandBehavior.Default,
        CancellationToken cancellationToken = default) =>
        await _dvlSqlConnection.ConnectAsync(
            dvlCommand => dvlCommand.ExecuteNonQueryAsync(timeout, cancellationToken),
            _procedureName,
            CommandType.StoredProcedure,
            parameters: _parameters);

    public async Task<TResult> ExecuteAsync<TResult>(Func<IDataReader, TResult> reader,
        int? timeout = default,
        CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default) =>
        await _dvlSqlConnection.ConnectAsync(
            dvlCommand => dvlCommand.ExecuteReaderAsync(reader, timeout, behavior, cancellationToken),
            _procedureName,
            CommandType.StoredProcedure,
            parameters: _parameters);

}
