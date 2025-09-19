using System.Data;
using System.Text;
using DvlSql.Expressions;
using static DvlSql.ExpressionHelpers;
using static System.Exts.Extensions;

namespace DvlSql.SqlServer;

internal class SqlDeletable(DvlSqlFromWithTableExpression fromExpression, IDvlSqlConnection dvlSqlConnection)
    : RemoveOutputable<int>(new DvlSqlDeleteExpression(fromExpression), dvlSqlConnection,
        (command, timeout, token) => command.ExecuteNonQueryAsync(timeout, token ?? CancellationToken.None)), IDeletable
{
    protected void SetOutputExpression(DvlSqlTableDeclarationExpression intoTable, string[] cols)
    {
        DeleteExpression.OutputExpression = OutputExp(intoTable, cols);
    }

    protected void SetOutputExpression(string[] cols)
    {
        DeleteExpression.OutputExpression = OutputExp(cols);
    }

    public IDeleteOutputable<TResult> Output<TResult>(Func<IDataReader, TResult> reader, params string[] cols)
    {
        SetOutputExpression(cols);
        return new RemoveOutputable<TResult>(DeleteExpression, DvlSqlConnection,
            (command, timeout, token) => command.ExecuteReaderAsync(reader, timeout, CommandBehavior.Default,
                token ?? CancellationToken.None));
    }

    public async Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default) =>
        await GetInsertDeleteExecutable().ExecuteAsync(timeout, cancellationToken);

    public IDeleteJoinable Join<T>(string tableName, DvlSqlComparisonExpression<T> compExpression)
    {
        DeleteExpression.AddJoin(InnerJoinExp(tableName, compExpression));
        return this;
    }

    public IDeleteJoinable Join<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
    {
        DeleteExpression.AddJoin(InnerJoinExp<T>(tableName, firstTableMatchingCol, secondTableMatchingCol));
        return this;
    }

    public IDeleteJoinable FullJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression)
    {
        DeleteExpression.AddJoin(FullJoinExp(tableName, compExpression));
        return this;
    }

    public IDeleteJoinable FullJoin<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
    {
        DeleteExpression.AddJoin(FullJoinExp<T>(tableName, firstTableMatchingCol, secondTableMatchingCol));
        return this;
    }

    public IDeleteJoinable LeftJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression)
    {
        DeleteExpression.AddJoin(LeftJoinExp(tableName, compExpression));
        return this;
    }

    public IDeleteJoinable LeftJoin<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
    {
        DeleteExpression.AddJoin(LeftJoinExp<T>(tableName, firstTableMatchingCol, secondTableMatchingCol));
        return this;
    }

    public IDeleteJoinable RightJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression)
    {
        DeleteExpression.AddJoin(RightJoinExp(tableName, compExpression));
        return this;
    }

    public IDeleteJoinable RightJoin<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
    {
        DeleteExpression.AddJoin(RightJoinExp<T>(tableName, firstTableMatchingCol, secondTableMatchingCol));
        return this;
    }
}

internal class RemoveOutputable<TResult>(
    DvlSqlDeleteExpression deleteExpression,
    IDvlSqlConnection dvlSqlConnection,
    Func<IDvlSqlCommand, int?, CancellationToken?,
        Task<TResult>> executeQuery) : IDeleteOutputable<TResult>
{
    protected readonly DvlSqlDeleteExpression DeleteExpression = deleteExpression;
    protected readonly IDvlSqlConnection DvlSqlConnection = dvlSqlConnection;
    private readonly Func<IDvlSqlCommand, int?, CancellationToken?, Task<TResult>> _executeQuery = executeQuery;

    protected IInsertDeleteExecutable<TResult> GetInsertDeleteExecutable() =>
        new SqlInsertDeleteExecutable<TResult>(DvlSqlConnection, ToString,
            GetDvlSqlParameters,
            _executeQuery);

    public IInsertDeleteExecutable<TResult> Where(DvlSqlBinaryExpression binaryExpression)
    {
        DeleteExpression.WhereExpression = new DvlSqlWhereExpression(binaryExpression);
        return GetInsertDeleteExecutable();
    }

    public IInsertDeleteExecutable<TResult> Where(DvlSqlBinaryExpression binaryExpression,
        IEnumerable<DvlSqlParameter> @params)
    {
        DeleteExpression.WhereExpression = new DvlSqlWhereExpression(binaryExpression).WithParameters(@params)
            as DvlSqlWhereExpression;
        return GetInsertDeleteExecutable();
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        var commandBuilder = new DvlSqlCommandBuilder(builder);

        DeleteExpression.Accept(commandBuilder);

        string result = builder.ToString().RemoveUnnecessaryNewlines();

        Statics.ParametersCount = 0;

        return result;
    }

    private IEnumerable<DvlSqlParameter> GetDvlSqlParameters() => DeleteExpression.WhereExpression?.Parameters
                                                                  ?? Enumerable.Empty<DvlSqlParameter>();
}