using DvlSql.Expressions;

namespace DvlSql;

// ReSharper disable once IdentifierTypo
public interface IJoinable
{
    ISelector Join<T>(string tableName, DvlSqlComparisonExpression<T> compExpression);
    ISelector Join<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    ISelector FullJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression);
    ISelector FullJoin<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    ISelector LeftJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression);
    ISelector LeftJoin<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    ISelector RightJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression);
    ISelector RightJoin<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    ISelector Join(string tableName, DvlSqlBinaryExpression binaryExpression);
    ISelector Join(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    ISelector FullJoin(string tableName, DvlSqlBinaryExpression binaryExpression);
    ISelector FullJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    ISelector LeftJoin(string tableName, DvlSqlBinaryExpression binaryExpression);
    ISelector LeftJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    ISelector RightJoin(string tableName, DvlSqlBinaryExpression binaryExpression);
    ISelector RightJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
}