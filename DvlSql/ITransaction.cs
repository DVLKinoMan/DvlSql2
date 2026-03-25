namespace DvlSql;

public interface ITransaction
{
    Task<IDvlSql> BeginTransactionAsync(CancellationToken token = default);
    Task<IDvlSql> BeginTransactionAsync(Func<IDvlSql, Task> action, CancellationToken token = default);
    Task ExecuteTransactionAsync(Func<IDvlSql, Task> action, CancellationToken token = default);
    Task CommitAsync(CancellationToken token = default);
    Task RollbackAsync(CancellationToken token = default);
}
