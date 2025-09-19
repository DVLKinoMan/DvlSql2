using DvlSql.Expressions;
using static DvlSql.ExpressionHelpers;

namespace DvlSql.SqlServer;

public partial class DvlSqlMs : IDvlSql
{
    private IDvlSqlConnection? _dvlSqlConnection;
    private readonly string? _connectionString;

    public DvlSqlMs(string connectionString) => _connectionString = connectionString;

    public DvlSqlMs(IDvlSqlConnection connection) => _dvlSqlConnection = connection;

    private IDvlSqlConnection GetConnection() => _dvlSqlConnection ?? new DvlSqlConnection(_connectionString ?? 
        throw new NullReferenceException($"{nameof(_connectionString)} can not be null"));
    
    public ISelector From(string tableName, bool withNoLock = false)
    {
        var fromExpression = FromExp(tableName, withNoLock);

        return new SqlSelector(fromExpression, GetConnection());
    }

    public ISelector From(DvlSqlFullSelectExpression @select) =>
        //var fromExpression = FromExp(@select);
        new SqlSelector(@select, GetConnection());

    public ISelector From(DvlSqlFromWithTableExpression fromWithTableExpression) =>
        //var fromExpression = FromExp(fromWithTableExpression);
        new SqlSelector(fromWithTableExpression, GetConnection());

    public IDeletable DeleteFrom(string tableName)
    {
        var fromExpression = FromExp(tableName);

        return new SqlDeletable(fromExpression, GetConnection());
    }

    public IDeletable DeleteFrom(DvlSqlFromWithTableExpression fromExpression) => new SqlDeletable(fromExpression, GetConnection());

    public IUpdateSetable Update(string tableName)
    {
        var updateExpression = new DvlSqlUpdateExpression(tableName);

        return new SqlUpdateable(GetConnection(), updateExpression);
    }

    public IDvlSql SetConnection(IDvlSqlConnection connection)
    {
        _dvlSqlConnection = connection;

        return this;
    }

    public IProcedureExecutable Procedure(string procedureName, params DvlSqlParameter[] parameters) =>
        new SqlProcedureExecutable(GetConnection(), procedureName, parameters);

    public async Task CommitAsync(CancellationToken token = default)
    {
        try
        {
            await _dvlSqlConnection!.CommitAsync(token);
        }
        catch (Exception exc)
        {
            var list = new List<Exception> {exc};
            try
            {
                await _dvlSqlConnection!.RollbackAsync(token);
            }
            catch (Exception exc2)
            {
                list.Add(exc2);
            }

            throw new AggregateException(list);
        }
        finally
        {
            await _dvlSqlConnection!.DisposeAsync();
            _dvlSqlConnection = null;
        }
    }

    public async Task RollbackAsync(CancellationToken token = default) =>
        await _dvlSqlConnection!.RollbackAsync(token);

    public async Task<IDvlSqlConnection> BeginTransactionAsync(CancellationToken token = default)
    {
        var conn = GetConnection().GetClone();
        await conn.BeginTransactionAsync(token);
        return conn;
    }

    public DvlSqlTableDeclarationExpression DeclareTable(string name) => new(name);
}