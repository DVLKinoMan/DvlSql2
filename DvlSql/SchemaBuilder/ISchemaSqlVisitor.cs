using DvlSql.Expressions;

namespace DvlSql;

public interface ISchemaBuilderVisitor
{
    void Visit(DvlSqlCreateTableExpression expression);
    void Visit(DvlSqlColumnExpression expression);
    void Visit(DvlSqlPrimaryKeyExpression expression);
    void Visit(DvlSqlDefaultExpression expression);
    void Visit(DvlSqlUniqueExpression expression);
    void Visit(DvlSqlIndexExpression expression);
    void Visit(DvlSqlForeignKeyExpression expression);
}