using System.Exts;
using System.Text;
using DvlSql.Expressions;
using static DvlSql.ExpressionHelpers;

namespace DvlSql.PostgreSql;

internal class SqlSelector : ISelector, IFilter, IGrouper, IUnionable, IFromable
{
    private readonly IDvlSqlConnection _dvlSqlConnection;
    private readonly DvlSqlUnionExpression _unionExpression = [];

    private DvlSqlFullSelectExpression CurrFullSelectExpression => _unionExpression.Last().Expression;

    public SqlSelector(DvlSqlFromExpression sqlFromExpression, IDvlSqlConnection dvlSqlConnection)
    {
        _unionExpression.Add(FullSelectExp(SelectExp(), sqlFromExpression));
        _dvlSqlConnection = dvlSqlConnection;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        var commandBuilder = new DvlSqlCommandBuilder(builder);

        _unionExpression.Accept(commandBuilder);

        string result = builder.ToString();

        Statics.ParametersCount = 0;

        return result;
    }

    public void NotExpOnFullSelects()
    {
        foreach (var (Expression, _) in _unionExpression)
            if (Expression.Where is { } where)
                Expression.Where = !where;
    }

    public ISelector From(string tableName, bool withNoLock = false)
    {
        _unionExpression.Add(FullSelectExp(SelectExp(), FromExp(tableName, withNoLock)));

        return this;
    }

    public ISelector From(DvlSqlFullSelectExpression @select)
    {
        _unionExpression.Add(@select);

        return this;
    }

    public ISelector From(DvlSqlFromWithTableExpression fromWithTableExpression)
    {
        _unionExpression.Add(FullSelectExp(SelectExp(), fromWithTableExpression));

        return this;
    }

    public List<DvlSqlParameter>? GetDvlSqlParameters()
    {
        var list = CurrFullSelectExpression.Where?.Parameters ?? [];

        if (CurrFullSelectExpression.GroupBy?.Parameters is not null)
            list = list.Concat(CurrFullSelectExpression.GroupBy.Parameters).ToList();

        return list.Count != 0 ? list : null;
    }

    public SqlSelector WithSelectTop(int num)
    {
        if (CurrFullSelectExpression.Select != null)
            CurrFullSelectExpression.Select.Top = num;
        return this;
    }

    public IOrderer Select(params string[] parameterNames)
    {
        if (CurrFullSelectExpression.Select == null)
            CurrFullSelectExpression.Select = SelectExp(parameterNames);
        else CurrFullSelectExpression.Select.AddRange(parameterNames);

        return new SqlOrderer(_dvlSqlConnection, this);
    }

    public IOrderer Select()
    {
        CurrFullSelectExpression.Select = SelectExp();

        return new SqlOrderer(_dvlSqlConnection, this);
    }

    public IOrderer SelectTop(int count, params string[] parameterNames)
    {
        CurrFullSelectExpression.Select = SelectExp(parameterNames, count);

        return new SqlOrderer(_dvlSqlConnection, this);
    }

    public IFilter Where(DvlSqlBinaryExpression binaryExpression)
    {
        CurrFullSelectExpression.Where =
            new DvlSqlWhereExpression(binaryExpression).WithParameters(
                    binaryExpression.Parameters.Select(p => p.WithName(p.Name.WithAlpha())).ToList()
                ) as
                DvlSqlWhereExpression;

        return this;
    }

    public IFilter Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params)
    {
        CurrFullSelectExpression.Where =
            new DvlSqlWhereExpression(binaryExpression).WithParameters(@params
                .Select(p => p.WithName(p.Name.WithAlpha())).ToList()) as DvlSqlWhereExpression;
        return this;
    }

    public ISelector Join<T>(string tableName, DvlSqlComparisonExpression<T> compExpression)
    {
        CurrFullSelectExpression.Join?.Add(InnerJoinExp(tableName, compExpression));
        return this;
    }

    public ISelector Join<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
    {
        CurrFullSelectExpression.Join?.Add(InnerJoinExp<T>(tableName, firstTableMatchingCol,
            secondTableMatchingCol));
        return this;
    }

    public ISelector FullJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression)
    {
        CurrFullSelectExpression.Join?.Add(FullJoinExp(tableName, compExpression));
        return this;
    }

    public ISelector FullJoin<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
    {
        CurrFullSelectExpression.Join?.Add(
            FullJoinExp<T>(tableName, firstTableMatchingCol, secondTableMatchingCol));
        return this;
    }

    public ISelector LeftJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression)
    {
        CurrFullSelectExpression.Join?.Add(LeftJoinExp(tableName, compExpression));
        return this;
    }

    public ISelector LeftJoin<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
    {
        CurrFullSelectExpression.Join?.Add(
            LeftJoinExp<T>(tableName, firstTableMatchingCol, secondTableMatchingCol));
        return this;
    }

    public ISelector RightJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression)
    {
        CurrFullSelectExpression.Join?.Add(RightJoinExp(tableName, compExpression));
        return this;
    }

    public ISelector RightJoin<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
    {
        CurrFullSelectExpression.Join?.Add(RightJoinExp<T>(tableName, firstTableMatchingCol,
            secondTableMatchingCol));
        return this;
    }


    public ISelector Join(string tableName, DvlSqlComparisonExpression compExpression)
    {
        CurrFullSelectExpression.Join?.Add(InnerJoinExp(tableName, compExpression));
        return this;
    }

    public ISelector Join(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
    {
        CurrFullSelectExpression.Join?.Add(InnerJoinExp(tableName, firstTableMatchingCol,
            secondTableMatchingCol));
        return this;
    }

    public ISelector FullJoin(string tableName, DvlSqlComparisonExpression compExpression)
    {
        CurrFullSelectExpression.Join?.Add(FullJoinExp(tableName, compExpression));
        return this;
    }

    public ISelector FullJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
    {
        CurrFullSelectExpression.Join?.Add(
            FullJoinExp(tableName, firstTableMatchingCol, secondTableMatchingCol));
        return this;
    }

    public ISelector LeftJoin(string tableName, DvlSqlComparisonExpression compExpression)
    {
        CurrFullSelectExpression.Join?.Add(LeftJoinExp(tableName, compExpression));
        return this;
    }

    public ISelector LeftJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
    {
        CurrFullSelectExpression.Join?.Add(
            LeftJoinExp(tableName, firstTableMatchingCol, secondTableMatchingCol));
        return this;
    }

    public ISelector RightJoin(string tableName, DvlSqlComparisonExpression compExpression)
    {
        CurrFullSelectExpression.Join?.Add(RightJoinExp(tableName, compExpression));
        return this;
    }

    public ISelector RightJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
    {
        CurrFullSelectExpression.Join?.Add(RightJoinExp(tableName, firstTableMatchingCol,
            secondTableMatchingCol));
        return this;
    }

    public IOrderer OrderBy(IOrderer orderBy, params string[] fields)
    {
        if (CurrFullSelectExpression.OrderBy == null)
            CurrFullSelectExpression.OrderBy =
                new DvlSqlOrderByExpression(fields.Select(f => (f, Ascending: Ordering.ASC)));
        else CurrFullSelectExpression.OrderBy.AddRange(fields.Select(f => (f, Ascending: Ordering.ASC)));

        return orderBy;
    }

    public IOrderer OrderByDescending(IOrderer orderBy, params string[] fields)
    {
        if (CurrFullSelectExpression.OrderBy == null)
            CurrFullSelectExpression.OrderBy =
                new DvlSqlOrderByExpression(fields.Select(f => (f, Descending: Ordering.DESC)));
        else CurrFullSelectExpression.OrderBy.AddRange(fields.Select(f => (f, Descending: Ordering.DESC)));

        return orderBy;
    }

    public ISelectExecutable Skip(IOrderer orderBy, int offsetRows, int? fetchNextRows = null)
    {
        CurrFullSelectExpression.Skip = new DvlSqlSkipExpression(offsetRows, fetchNextRows);

        return orderBy;
    }

    public IGrouper GroupBy(params string[] parameterNames)
    {
        CurrFullSelectExpression.GroupBy = new DvlSqlGroupByExpression(parameterNames);

        return this;
    }

    public ISelectable Having(DvlSqlGroupByBinaryExpression binaryExpression)
    {
        CurrFullSelectExpression.GroupBy!.BinaryExpression = binaryExpression;
        CurrFullSelectExpression.GroupBy.WithParameters(binaryExpression.Parameters);

        return this;
    }

    public ISelectable Having(DvlSqlGroupByBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params)
    {
        CurrFullSelectExpression.GroupBy!.BinaryExpression = binaryExpression;
        CurrFullSelectExpression.GroupBy.WithParameters(@params);

        return this;
    }

    public IFromable Union()
    {
        var (Expression, _) = _unionExpression.Last();
        _unionExpression.RemoveAt(_unionExpression.Count - 1);
        _unionExpression.Add((Expression, UnionType.Union));

        return this;
    }

    public IFromable UnionAll()
    {
        var (Expression, _) = _unionExpression.Last();
        _unionExpression.RemoveAt(_unionExpression.Count - 1);
        _unionExpression.Add((Expression, UnionType.UnionAll));

        return this;
    }
}