namespace DvlSql;

public interface ISchemaExecutable
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}