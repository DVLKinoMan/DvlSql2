using DvlSql.Expressions;

namespace DvlSql;

public interface ICodeBuilder
{
    void Visit(DvlSqlAlterTableExpression expression);
    void Visit(DvlSqlDropTableExpression expression);
    void Visit(DvlSqlRenameTableExpression expression);
    void Visit(DvlSqlCreateTableExpression expression);

    string ToString();
}