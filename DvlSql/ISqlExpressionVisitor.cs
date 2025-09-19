using System.Runtime.CompilerServices;
using DvlSql.Expressions;

namespace DvlSql;

public interface ISqlExpressionVisitor
{
    void Visit(DvlSqlInExpression expression);
    void Visit(DvlSqlOrExpression expression);
    void Visit(DvlSqlGroupByOrExpression expression);
    void Visit(DvlSqlAndExpression expression);
    void Visit(DvlSqlGroupByAndExpression expression);
    void Visit(DvlSqlSelectExpression expression);
    void Visit(DvlSqlWhereExpression expression);
    void Visit<T>(DvlSqlComparisonExpression<T> expression);
    void Visit(DvlSqlComparisonExpression expression);
    void Visit<T>(DvlSqlGroupByComparisonExpression<T> expression);
    void Visit(DvlSqlGroupByComparisonExpression expression);
    void Visit(DvlSqlConstantColumnExpression expression);
    void Visit<TValue>(DvlSqlConstantExpression<TValue> expression);
    void Visit<TValue>(DvlSqlMemberExpression<TValue> expression);
    void Visit<TValue>(DvlSqlGroupByMemberExpression<TValue> expression);

    void Visit(DvlSqlFromExpression expression);
    void Visit<T>(DvlSqlJoinExpression<T> expression);
    void Visit(DvlSqlGeneralJoinExpression expression);
    void Visit(DvlSqlOrderByExpression expression);

    void Visit(DvlSqlGroupByExpression expression);

    //void Visit(DvlSqlNotExpression expression);
    void Visit(DvlSqlLikeExpression expression);
    void Visit(DvlSqlIsNullExpression expression);
    void Visit<TParam>(DvlSqlInsertIntoExpression<TParam> expression) where TParam : ITuple;
    void Visit(DvlSqlInsertIntoSelectExpression expression);
    void Visit(DvlSqlFullSelectExpression expression);
    void Visit(DvlSqlDeleteExpression expression);
    void Visit(DvlSqlUpdateExpression expression);
    void Visit(DvlSqlUnionExpression expression);
    void Visit(DvlSqlExistsExpression expression);
    void Visit(DvlSqlSkipExpression expression);
    void Visit(DvlSqlTableDeclarationExpression expression);
    void Visit(DvlSqlOutputExpression expression);
    void Visit<T>(DvlSqlValuesExpression<T> expression) where T : ITuple;
    void Visit(DvlSqlAsExpression expression);
    void Visit(DvlSqlBinaryEmptyExpression expression);
    void Visit(DvlSqlGroupByBinaryEmptyExpression expression);
    void Visit<T>(DvlSqlAverageExpression<T> expression);
    void Visit<T>(DvlSqlSumExpression<T> expression);
    void Visit(DvlSqlCountExpression expression);
    void Visit<T>(DvlSqlMinExpression<T> expression);
    void Visit<T>(DvlSqlMaxExpression<T> expression);
}