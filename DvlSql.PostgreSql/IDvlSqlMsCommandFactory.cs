using System.Data;
using System.Data.Common;
using Npgsql;

namespace DvlSql.PostgreSql;

public interface IDvlSqlMsCommandFactory
{
    IDvlSqlCommand CreateSqlCommand(CommandType commandType, NpgsqlConnection connection,
        string sqlString, DbTransaction? transaction = null, params NpgsqlParameter[]? parameters);
}