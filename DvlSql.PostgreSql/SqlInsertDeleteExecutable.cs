namespace DvlSql.PostgreSql;

internal class SqlInsertDeleteExecutable<TResult>(IDvlSqlConnection connection, 
        Func<string> sqlStringFunc, 
        Func<IEnumerable<DvlSqlParameter>> getDvlSqlParameters, 
        Func<IDvlSqlCommand, int?, CancellationToken?, Task<TResult>> reader) : IInsertDeleteExecutable<TResult>
{
    private readonly IDvlSqlConnection _connection = connection;
    private readonly Func<string> _sqlStringFunc = sqlStringFunc;
    private readonly Func<IEnumerable<DvlSqlParameter>> _getDvlSqlParameters = getDvlSqlParameters;
    private readonly Func<IDvlSqlCommand, int?, CancellationToken?, Task<TResult>> _reader = reader;

    public async Task<TResult> ExecuteAsync(int? timeout = default, 
        CancellationToken cancellationToken = default) =>
        await _connection.ConnectAsync(
            dvlCommand => _reader(dvlCommand, timeout, cancellationToken), 
            _sqlStringFunc(),
            parameters: _getDvlSqlParameters().ToArray());

    public override string ToString() => _sqlStringFunc();
}