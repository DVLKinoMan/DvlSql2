namespace DvlSql;

public interface ISchemaExecutable
{
    Task ExecuteAsync(int? timeout = null, CancellationToken cancellationToken = default);
}