using DvlSql.Expressions;

namespace DvlSql;

public interface ICodeBuilder
{
    void Visit(DvlSqlAlterColumnExpression expression);
    void Visit(DvlSqlAlterTableExpression expression);
    void Visit(DvlSqlDropTableExpression expression);
    void Visit(DvlSqlRenameTableExpression expression);
    void Visit(DvlSqlCreateTableExpression expression);
    void Visit(DvlSqlCreateColumnExpression expression);
    void Visit(DvlSqlForeignKeyExpression expression);
    void Visit(DvlSqlIndexExpression expression);
    void Visit(DvlSqlPrimaryKeyExpression expression);
    void Visit(DvlSqlUniqueExpression expression);

    string ToString();
}