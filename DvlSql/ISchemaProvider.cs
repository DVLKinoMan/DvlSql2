using DvlSql.Expressions;

namespace DvlSql;

public interface ISchemaProvider
{
    Task<List<DvlSqlCreateTableExpression>> GetAllTablesAsync();
    Task<DvlSqlCreateTableExpression?> GetTableAsync(string tableName);
    Task<List<DvlSqlCreateColumnExpression>> GetColumnsAsync(string tableName);
    Task<DvlSqlCreateColumnExpression?> GetColumnAsync(string tableName, string columnName);
}