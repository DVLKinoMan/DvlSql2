using DvlSql.Expressions;

namespace DvlSql;

public interface IAlterTableVisitor
{
    void Visit(DvlSqlAlterTableExpression expression);
    void Visit(DvlSqlCreateColumnExpression expression);
    void Visit(DvlSqlAlterColumnExpression expression);
    void Visit(DvlSqlPrimaryKeyExpression expression);
    void Visit(DvlSqlDefaultExpression expression);
    void Visit(DvlSqlUniqueExpression expression);
    void Visit(DvlSqlIndexExpression expression);
    void Visit(DvlSqlForeignKeyExpression expression);
    void Visit(DvlSqlDropConstraintExpression expression);
    void Visit(DvlSqlDropIndexExpression expression);
    void Visit(DvlSqlRenameColumnExpression expression);
    void Visit(DvlSqlRenameTableExpression expression);
    void Visit(DvlSqlDropColumnExpression expression);
    void Visit(DvlSqlDropTableExpression expression);
}