using System.Data;
using System.Data.Common;
using Npgsql;

namespace DvlSql.PostgreSql;

internal class DvlSqlMsCommandFactory : IDvlSqlMsCommandFactory
{
    public IDvlSqlCommand CreateSqlCommand(CommandType commandType, NpgsqlConnection connection, string sqlString, DbTransaction? transaction = null,
        params NpgsqlParameter[]? parameters)
    {
        var command = new NpgsqlCommand(sqlString, connection)
        {
            CommandType = commandType
        };

        if (transaction != null)
            command.Transaction = (NpgsqlTransaction)transaction;

        if (parameters != null)
            foreach (var parameter in parameters)
                command.Parameters.Add(parameter);

        return new DvlSqlCommand(command);
    }
}