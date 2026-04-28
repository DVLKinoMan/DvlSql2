using System.Text;
using DvlSql.Expressions;

namespace DvlSql.SqlServer;

public partial class DvlSqlMs : ISchemaExecutable
{
    private DvlSqlRenameTableExpression? _renameTableExpression;
    private DvlSqlDropTableExpression? _dropTableExpression;

    public ITableCreator CreateTable(string tableName) => new DvlSqlTableCreator(tableName, this);

    public ITableCreator CreateTable(string associatedName, string tableName) =>
        new DvlSqlTableCreator(tableName, associatedName, this);

    public ITableAlterer AlterTable(string tableName) => new DvlSqlTableAlterer(tableName, this);

    public ITableAlterer AlterTable(string associatedName, string tableName) => new DvlSqlTableAlterer(tableName, this);

    public ISchemaExecutable RenameTable(string oldTableName, string newTableName)
    {
        _renameTableExpression = new(oldTableName, newTableName);
        return this;
    }

    public ISchemaExecutable RenameTable(string associatedName, string oldTableName, string newTableName)
    {
        _renameTableExpression = new(oldTableName, newTableName);
        return this;
    }

    public ISchemaExecutable DropTable(string tableName)
    {
        _dropTableExpression = new(tableName);
        return this;
    }

    public ISchemaExecutable DropTable(string associatedName, string tableName)
    {
        _dropTableExpression = new(tableName);
        return this;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        var commandBuilder = new DvlSqlAlterTableBuilder(builder);

        _renameTableExpression?.Accept(commandBuilder);
        _dropTableExpression?.Accept(commandBuilder);

        string result = builder.ToString();

        Statics.ParametersCount = 0;

        return result;
    }

    public async Task ExecuteAsync(int? timeout = null, CancellationToken cancellationToken = default)
        => await this.ExecuteSqlAsync(ToString(), timeout, cancellationToken);
}