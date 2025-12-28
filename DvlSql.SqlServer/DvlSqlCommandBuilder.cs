using System.Runtime.CompilerServices;
using System.Text;
using DvlSql.Expressions;
using static System.Exts.Extensions;

namespace DvlSql.SqlServer;

internal class DvlSqlCommandBuilder(StringBuilder command) : ISqlExpressionVisitor
{
    private readonly StringBuilder _command = command;

    public void Visit(DvlSqlFromExpression expression)
    {
        switch (expression)
        {
            case DvlSqlFromWithTableExpression { } fromWithTable:
                _command.Append($"FROM {fromWithTable.TableName}");
                expression.As?.Accept(this);
                if (fromWithTable.WithNoLock)
                    _command.Append(" WITH(NOLOCK)");
                break;
            default:
                _command.Append("FROM (");
                if (expression is DvlSqlFullSelectExpression { } select)
                    Visit(select);
                else expression.Accept(this);
                _command.Append(")");
                expression.As?.Accept(this);
                break;
        }
    }

    public void Visit(DvlSqlGroupByAndExpression expression)
    {
        string op = expression.Not ? " OR " : " AND ";

        _command.TrimIfLastCharacterIs('(');
        _command.Append("( ");
        foreach (var innerExpression in expression.InnerExpressions)
        {
            if (innerExpression is DvlSqlGroupByBinaryEmptyExpression)
                continue;
            innerExpression.Accept(this);
            _command.Append(op);
        }

        _command.Remove(_command.Length - op.Length, op.Length);
        _command.Append(_command[^1] == ')' ? ")" : " )");
    }

    public void Visit(DvlSqlSelectExpression expression)
    {
        _command.Append("SELECT ");

        if (expression.Top != null)
            _command.Append($"TOP {expression.Top} ");

        if (expression.ParameterNames.Count == 0)
        {
            _command.Append("* ");
            //goto end;
            return;
        }

        _command.Append(string.Join(", ", expression.ParameterNames.Where(p => !string.IsNullOrEmpty(p))));
        _command.Append(' ');

        //end:
        //expression.From.Accept(this);
    }

    public void Visit(DvlSqlWhereExpression expression)
    {
        switch (expression.InnerExpression)
        {
            case DvlSqlAndExpression andExp when !andExp.InnerExpressions.Any():
            case DvlSqlOrExpression orExp when !orExp.InnerExpressions.Any():
                return;
            default:
                _command.Append(expression.IsRoot ? $"{Environment.NewLine}WHERE " : " WHERE ");

                expression.InnerExpression.Accept(this);
                break;
        }
    }

    public void Visit(DvlSqlGroupByComparisonExpression expression)
    {
        expression.LeftExpression.Accept(this);

        _command.Append(
            (expression.Not ? GetNotExp(expression.ComparisonOperator) : expression.ComparisonOperator) switch
            {
                SqlComparisonOperator.Equality => " = ",
                SqlComparisonOperator.Greater => " > ",
                SqlComparisonOperator.GreaterOrEqual => " >= ",
                SqlComparisonOperator.Less => " < ",
                SqlComparisonOperator.LessOrEqual => " <= ",
                SqlComparisonOperator.NotEquality => " != ",
                SqlComparisonOperator.Different => " <> ",
                SqlComparisonOperator.NotGreater => " !< ",
                SqlComparisonOperator.NotLess => " !> ",
                _ => throw new NotImplementedException("ComparisonOperator not implemented")
            });

        expression.RightExpression.Accept(this);

        static SqlComparisonOperator GetNotExp(SqlComparisonOperator op) => op switch
        {
            SqlComparisonOperator.Different => SqlComparisonOperator.Equality,
            SqlComparisonOperator.Equality => SqlComparisonOperator.Different,
            SqlComparisonOperator.Less => SqlComparisonOperator.GreaterOrEqual,
            SqlComparisonOperator.Greater => SqlComparisonOperator.LessOrEqual,
            SqlComparisonOperator.GreaterOrEqual => SqlComparisonOperator.Less,
            SqlComparisonOperator.LessOrEqual => SqlComparisonOperator.Greater,
            SqlComparisonOperator.NotLess => SqlComparisonOperator.GreaterOrEqual,
            SqlComparisonOperator.NotGreater => SqlComparisonOperator.LessOrEqual,
            SqlComparisonOperator.NotEquality => SqlComparisonOperator.Equality,
            _ => throw new NotImplementedException("ComparisonOperator not implemented")
        };
    }

    public void Visit(DvlSqlConstantColumnExpression expression) => _command.Append(expression.Value);

    public void Visit<TValue>(DvlSqlConstantExpression<TValue> expression) =>
        _command.Append(expression.ToString().WithAlpha());

    public void Visit<TValue>(DvlSqlMemberExpression<TValue> expression) =>
        _command.Append(expression.MemberName);

    public void Visit<TValue>(DvlSqlGroupByMemberExpression<TValue> expression) =>
        _command.Append(expression.MemberName);

    public void Visit<T>(DvlSqlJoinExpression<T> expression)
    {
        string joinCommand = expression switch
        {
            DvlSqlFullJoinExpression<T> _ => "FULL OUTER JOIN",
            DvlSqlInnerJoinExpression<T> _ => "INNER JOIN",
            DvlSqlLeftJoinExpression<T> _ => "LEFT OUTER JOIN",
            DvlSqlRightJoinExpression<T> _ => "RIGHT OUTER JOIN",
            _ => throw new NotImplementedException("JoinExpression not implemented")
        };

        _command.Append(expression.IsRoot
            ? $"{Environment.NewLine}{joinCommand} {expression.TableName} ON "
            : $" {joinCommand} {expression.TableName} ON ");
        expression.ComparisonExpression.Accept(this);
    }

    public void Visit(DvlSqlGeneralJoinExpression expression)
    {
        string joinCommand = expression switch
        {
            DvlSqlFullJoinExpression _ => "FULL OUTER JOIN",
            DvlSqlInnerJoinExpression _ => "INNER JOIN",
            DvlSqlLeftJoinExpression _ => "LEFT OUTER JOIN",
            DvlSqlRightJoinExpression _ => "RIGHT OUTER JOIN",
            _ => throw new NotImplementedException("JoinExpression not implemented")
        };

        _command.Append(expression.IsRoot
            ? $"{Environment.NewLine}{joinCommand} {expression.TableName} ON "
            : $" {joinCommand} {expression.TableName} ON ");
        expression.ComparisonExpression.Accept(this);
    }

    public void Visit(DvlSqlOrderByExpression expression)
    {
        _command.Append(expression.IsRoot ? $"{Environment.NewLine}ORDER BY " : " ORDER BY ");
        foreach (var (column, ordering) in expression.Params)
            _command.Append($"{column} {ordering}, ");

        if (expression.Params.Count != 0)
            _command.Remove(_command.Length - 2, 2);
    }

    public void Visit(DvlSqlGroupByExpression expression)
    {
        _command.Append(expression.IsRoot ? $"{Environment.NewLine}GROUP BY " : " GROUP BY ");

        foreach (var parameterName in expression.ParameterNames)
            _command.Append($"{parameterName}, ");

        if (expression.ParameterNames.Count != 0)
            _command.Remove(_command.Length - 2, 2);

        if (expression.BinaryExpression != null)
        {
            _command.Append($"{Environment.NewLine}HAVING ");
            expression.BinaryExpression.Accept(this);
        }
    }

    public void Visit<TParam>(DvlSqlInsertIntoExpression<TParam> expression) where TParam : ITuple
    {
        expression.OutputExpression?.IntoTable?.Accept(this);

        _command.Append($"INSERT INTO {expression.TableName}");

        _command.Append(" ( ");

        foreach (var column in expression.Columns)
            _command.Append($"{column}, ");

        if (expression.Columns.Length != 0)
            _command.Remove(_command.Length - 2, 2);

        _command.Append(" )");

        expression.OutputExpression?.Accept(this);
        expression.ValuesExpression?.Accept(this);
    }

    public void Visit(DvlSqlInsertIntoSelectExpression expression)
    {
        _command.Append($"INSERT INTO {expression.TableName}");

        _command.Append(" ( ");

        foreach (var column in expression.Columns)
            _command.Append($"{column}, ");

        if (expression.Columns.Length != 0)
            _command.Remove(_command.Length - 2, 2);

        _command.Append(" ) ");

        expression.OutputExpression?.Accept(this);

        expression.SelectExpression?.Accept(this);
        _command.TrimEnd();
    }

    public void Visit(DvlSqlFullSelectExpression expression)
    {
        if (expression.Select == null)
            throw new ArgumentNullException(nameof(expression.Select), "expression has no Select Expression");

        expression.Select.Accept(this);
        expression.From.Accept(this);
        if (expression.Join != null)
            foreach (var joinExpression in expression.Join)
                joinExpression.Accept(this);
        expression.Where?.Accept(this);
        expression.GroupBy?.Accept(this);
        expression.OrderBy?.Accept(this);
        expression.Skip?.Accept(this);
    }

    public void Visit(DvlSqlDeleteExpression expression)
    {
        _command.Append(
            $"DELETE {(expression.FromExpression.As != null ? $"{expression.FromExpression.As.Name} " : expression.Join?.Count != 0 ? expression.FromExpression.TableName : "")}");
        expression.OutputExpression?.Accept(this);
        expression.FromExpression.Accept(this);
        if (expression.Join?.Count != 0)
        {
            if (expression.OutputExpression != null)
                throw new("Joins can not be when Output Expression exists");
            expression.Join?.ForEach(j => j.Accept(this));
        }

        expression.WhereExpression?.Accept(this);
    }

    public void Visit(DvlSqlUpdateExpression expression)
    {
        _command.Append(
            $"UPDATE {(expression.FromExpression.As != null ? expression.FromExpression.As.Name : expression.FromExpression.TableName)}");
        _command.Append($"{Environment.NewLine}SET ");
        foreach (var sqlParam in expression.DvlSqlParameters)
            _command.Append(
                sqlParam switch
                {
                    DvlSqlParameter<string> { ExactValue: true } stringParam =>
                        $"{stringParam.Name} = {stringParam.Value}, ",
                    _ => $"{sqlParam.Name} = {sqlParam.Name.WithAlpha()}, "
                });

        _command.Remove(_command.Length - 2, 2);
        if (expression.FromExpression.As != null)
        {
            _command.Append(Environment.NewLine);
            expression.FromExpression.Accept(this);
        }
        expression.WhereExpression?.Accept(this);
    }

    public void Visit(DvlSqlUnionExpression expression)
    {
        foreach (var (selectExpression, type) in expression)
        {
            selectExpression.Accept(this);
            if (type != null)
                _command.AppendLine(
                    $"{Environment.NewLine}{(type == UnionType.Union ? "UNION" : "UNION ALL")}");
        }
    }

    public void Visit(DvlSqlSkipExpression expression)
    {
        _command.Append($"{Environment.NewLine}OFFSET {expression.OffsetRows} ROWS");
        if (expression.FetchNextRows != null)
            _command.Append($" FETCH NEXT {expression.FetchNextRows} ROWS ONLY");
    }

    public void Visit(DvlSqlTableDeclarationExpression expression)
    {
        _command.Append($"{Environment.NewLine}DECLARE {expression.TableName} TABLE (");

        foreach (var col in expression.Columns)
            _command.Append(
                $"{Environment.NewLine}{col.Name} {col.SqlDbType}{(col.Size != null ? $"({col.Size})" : "")} {(col.IsNotNull ? "NOT NULL" : "NULL")},");

        if (expression.Columns.Count > 0)
            _command.Remove(_command.Length - 1, 1);

        _command.Append(");");
    }

    public void Visit(DvlSqlOutputExpression expression)
    {
        _command.Append($"{Environment.NewLine}OUTPUT {string.Join(',', expression.Columns)}");
        if (expression.IntoTable != null)
            _command.Append($"{Environment.NewLine}INTO {expression.IntoTable.TableName}");
        _command.Append(Environment.NewLine);
    }

    public void Visit<T>(DvlSqlValuesExpression<T> expression) where T : ITuple
    {
        bool hasAs = expression.As != null;
        _command.Append($"{Environment.NewLine}{(hasAs ? $"FROM{Environment.NewLine}(" : "")}VALUES");
        int count = 0;
        int? len = null;
        foreach (var value in expression.Values)
        {
            _command.Append($"{Environment.NewLine}( ");

            for (int i = 0; i < GetLen(value); i++, count++)
                _command.Append($"{expression.SqlParameters[count].Name}, ");

            _command.Remove(_command.Length - 2, 2);
            _command.Append(" ),");
        }

        if (expression.Values.Length != 0)
            _command.Remove(_command.Length - 1, 1);
        _command.Append(hasAs ? ")" : "");

        expression.As?.Accept(this);

        int GetLen(ITuple value)
        {
            if (len is { } i)
                return i;

            return (int)(len = value.Length == 8 && value[7] is ITuple tup ? 7 + GetLen(tup) : value.Length);
        }
    }

    public void Visit(DvlSqlAsExpression expression)
    {
        _command.Append($" {(expression.UseAsKeyword ? "AS" : "")} {expression.Name}");
        if (expression.Parameters != null)
            _command.Append($"({string.Join(", ", expression.Parameters)})");
    }

    #region BinaryExpressions

    public void Visit(DvlSqlInExpression expression)
    {
        _command.Append($"{expression.ParameterName}{(expression.Not ? " NOT" : "")} IN ( ");

        bool isEmpty = true;

        foreach (var innerExpression in expression.InnerExpressions)
        {
            if (innerExpression is DvlSqlBinaryEmptyExpression)
                continue;
            isEmpty = false;
            innerExpression.Accept(this);
            _command.Append(", ");
        }

        if (!isEmpty)
            _command.Remove(_command.Length - 2, 2);

        _command.Append(" )");
    }

    public void Visit(DvlSqlOrExpression expression)
    {
        string op = expression.Not ? " AND " : " OR ";

        _command.TrimIfLastCharacterIs('(');
        _command.Append("( ");
        foreach (var innerExpression in expression.InnerExpressions)
        {
            if (innerExpression is DvlSqlBinaryEmptyExpression)
                continue;
            innerExpression.Accept(this);
            _command.Append(op);
        }

        _command.Remove(_command.Length - op.Length, op.Length);
        _command.Append(_command[^1] == ')' ? ")" : " )");
    }

    public void Visit(DvlSqlGroupByOrExpression expression)
    {
        string op = expression.Not ? " AND " : " OR ";

        _command.TrimIfLastCharacterIs('(');
        _command.Append("( ");
        foreach (var innerExpression in expression.InnerExpressions)
        {
            if (innerExpression is DvlSqlGroupByBinaryEmptyExpression)
                continue;
            innerExpression.Accept(this);
            _command.Append(op);
        }

        _command.Remove(_command.Length - op.Length, op.Length);
        _command.Append(_command[^1] == ')' ? ")" : " )");
    }

    public void Visit(DvlSqlAndExpression expression)
    {
        string op = expression.Not ? " OR " : " AND ";

        _command.TrimIfLastCharacterIs('(');
        _command.Append("( ");
        foreach (var innerExpression in expression.InnerExpressions)
        {
            if (innerExpression is DvlSqlBinaryEmptyExpression)
                continue;
            innerExpression.Accept(this);
            _command.Append(op);
        }

        _command.Remove(_command.Length - op.Length, op.Length);
        _command.Append(_command[^1] == ')' ? ")" : " )");
    }

    public void Visit<T>(DvlSqlComparisonExpression<T> expression)
    {
        expression.LeftExpression.Accept(this);

        _command.Append(
            (expression.Not ? GetNotExp(expression.ComparisonOperator) : expression.ComparisonOperator) switch
            {
                SqlComparisonOperator.Equality => " = ",
                SqlComparisonOperator.Greater => " > ",
                SqlComparisonOperator.GreaterOrEqual => " >= ",
                SqlComparisonOperator.Less => " < ",
                SqlComparisonOperator.LessOrEqual => " <= ",
                SqlComparisonOperator.NotEquality => " != ",
                SqlComparisonOperator.Different => " <> ",
                SqlComparisonOperator.NotGreater => " !< ",
                SqlComparisonOperator.NotLess => " !> ",
                _ => throw new NotImplementedException("ComparisonOperator not implemented")
            });

        expression.RightExpression.Accept(this);

        static SqlComparisonOperator GetNotExp(SqlComparisonOperator op) => op switch
        {
            SqlComparisonOperator.Different => SqlComparisonOperator.Equality,
            SqlComparisonOperator.Equality => SqlComparisonOperator.Different,
            SqlComparisonOperator.Less => SqlComparisonOperator.GreaterOrEqual,
            SqlComparisonOperator.Greater => SqlComparisonOperator.LessOrEqual,
            SqlComparisonOperator.GreaterOrEqual => SqlComparisonOperator.Less,
            SqlComparisonOperator.LessOrEqual => SqlComparisonOperator.Greater,
            SqlComparisonOperator.NotLess => SqlComparisonOperator.GreaterOrEqual,
            SqlComparisonOperator.NotGreater => SqlComparisonOperator.LessOrEqual,
            SqlComparisonOperator.NotEquality => SqlComparisonOperator.Equality,
            _ => throw new NotImplementedException("ComparisonOperator not implemented")
        };
    }

    public void Visit(DvlSqlComparisonExpression expression)
    {
        expression.LeftExpression.Accept(this);

        _command.Append(
            (expression.Not ? GetNotExp(expression.ComparisonOperator) : expression.ComparisonOperator) switch
            {
                SqlComparisonOperator.Equality => " = ",
                SqlComparisonOperator.Greater => " > ",
                SqlComparisonOperator.GreaterOrEqual => " >= ",
                SqlComparisonOperator.Less => " < ",
                SqlComparisonOperator.LessOrEqual => " <= ",
                SqlComparisonOperator.NotEquality => " != ",
                SqlComparisonOperator.Different => " <> ",
                SqlComparisonOperator.NotGreater => " !< ",
                SqlComparisonOperator.NotLess => " !> ",
                _ => throw new NotImplementedException("ComparisonOperator not implemented")
            });

        expression.RightExpression.Accept(this);

        static SqlComparisonOperator GetNotExp(SqlComparisonOperator op) => op switch
        {
            SqlComparisonOperator.Different => SqlComparisonOperator.Equality,
            SqlComparisonOperator.Equality => SqlComparisonOperator.Different,
            SqlComparisonOperator.Less => SqlComparisonOperator.GreaterOrEqual,
            SqlComparisonOperator.Greater => SqlComparisonOperator.LessOrEqual,
            SqlComparisonOperator.GreaterOrEqual => SqlComparisonOperator.Less,
            SqlComparisonOperator.LessOrEqual => SqlComparisonOperator.Greater,
            SqlComparisonOperator.NotLess => SqlComparisonOperator.GreaterOrEqual,
            SqlComparisonOperator.NotGreater => SqlComparisonOperator.LessOrEqual,
            SqlComparisonOperator.NotEquality => SqlComparisonOperator.Equality,
            _ => throw new NotImplementedException("ComparisonOperator not implemented")
        };
    }

    public void Visit<T>(DvlSqlGroupByComparisonExpression<T> expression)
    {
        expression.LeftExpression.Accept(this);

        _command.Append(
            (expression.Not ? GetNotExp(expression.ComparisonOperator) : expression.ComparisonOperator) switch
            {
                SqlComparisonOperator.Equality => " = ",
                SqlComparisonOperator.Greater => " > ",
                SqlComparisonOperator.GreaterOrEqual => " >= ",
                SqlComparisonOperator.Less => " < ",
                SqlComparisonOperator.LessOrEqual => " <= ",
                SqlComparisonOperator.NotEquality => " != ",
                SqlComparisonOperator.Different => " <> ",
                SqlComparisonOperator.NotGreater => " !< ",
                SqlComparisonOperator.NotLess => " !> ",
                _ => throw new NotImplementedException("ComparisonOperator not implemented")
            });

        expression.RightExpression.Accept(this);

        static SqlComparisonOperator GetNotExp(SqlComparisonOperator op) => op switch
        {
            SqlComparisonOperator.Different => SqlComparisonOperator.Equality,
            SqlComparisonOperator.Equality => SqlComparisonOperator.Different,
            SqlComparisonOperator.Less => SqlComparisonOperator.GreaterOrEqual,
            SqlComparisonOperator.Greater => SqlComparisonOperator.LessOrEqual,
            SqlComparisonOperator.GreaterOrEqual => SqlComparisonOperator.Less,
            SqlComparisonOperator.LessOrEqual => SqlComparisonOperator.Greater,
            SqlComparisonOperator.NotLess => SqlComparisonOperator.GreaterOrEqual,
            SqlComparisonOperator.NotGreater => SqlComparisonOperator.LessOrEqual,
            SqlComparisonOperator.NotEquality => SqlComparisonOperator.Equality,
            _ => throw new NotImplementedException("ComparisonOperator not implemented")
        };
    }

    public void Visit(DvlSqlLikeExpression expression)
    {
        string likeStr = expression.Not ? "NOT LIKE" : "LIKE";
        _command.Append($"{expression.Field} {likeStr} '{expression.Pattern.GetEscapedString()}'");
    }

    public void Visit(DvlSqlIsNullExpression expression)
    {
        expression.Expression.Accept(this);
        _command.Append(expression.Not ? " IS NOT NULL" : " IS NULL");
    }

    public void Visit(DvlSqlExistsExpression expression)
    {
        _command.Append($"{(expression.Not ? "NOT " : "")}EXISTS( ");
        expression.Select.Accept(this);
        _command.Append(" )");
    }

    public void Visit(DvlSqlBinaryEmptyExpression expression)
    {
    }

    public void Visit(DvlSqlGroupByBinaryEmptyExpression expression)
    {
    }

    #endregion

    public void Visit<T>(DvlSqlAverageExpression<T> expression) =>
        _command.Append($"AVG({expression.MemberName})");

    public void Visit<T>(DvlSqlSumExpression<T> expression) =>
        _command.Append($"SUM({expression.MemberName})");

    public void Visit(DvlSqlCountExpression expression) =>
        _command.Append($"COUNT({expression.MemberName})");

    public void Visit<T>(DvlSqlMinExpression<T> expression) =>
        _command.Append($"MIN({expression.MemberName})");

    public void Visit<T>(DvlSqlMaxExpression<T> expression) =>
        _command.Append($"MAX({expression.MemberName})");
}