using System.Data;

namespace DvlSql;

public interface IDvlSqlCommand : IDisposable
{
    Task<int> ExecuteNonQueryAsync(int? timeout = default, CancellationToken cancellationToken = default);

    Task<TResult> ExecuteReaderAsync<TResult>(Func<IDataReader, TResult> converterFunc, int? timeout = default,
        CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default);

    Task<TResult> ExecuteScalarAsync<TResult>(Func<object, TResult> converterFunc, int? timeout = default, CancellationToken cancellationToken = default);
    
    string CommandText { get; }
}
