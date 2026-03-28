using System.Data;
using System.Text;
using DvlSql.Expressions;

namespace DvlSql.SqlServer;

internal class DvlSqlTableCreator : ITableCreator, IColumnCreator
{
    private readonly IDvlSql _dvlSql;
    private readonly DvlSqlCreateTableExpression _createTableExpression;
    private DvlSqlColumnExpression _currentColumn;

    public DvlSqlTableCreator(string tableName, IDvlSql dvlSql)
    {
        _dvlSql = dvlSql;
        _createTableExpression = new(tableName);
    }

    public IColumnCreator WithColumn(string name)
    {
        _currentColumn = new(name);
        _createTableExpression.ColumnExpressions.Add(_currentColumn);
        return this;
    }

    public IColumnCreator WithColumn(string associatedName, string name)
    {
        _currentColumn = new(name);
        _createTableExpression.ColumnExpressions.Add(_currentColumn);
        return this;
    }

    public IColumnCreator AsType(DvlSqlType type)
    {
        _currentColumn.Type = type.SqlDbType;
        _currentColumn.Size = type.Size;
        _currentColumn.Precision = type.Precision;
        return this;
    }

    public IColumnCreator AsType(SqlDbType type, int? size = null, byte? precision = null, byte? scale = null)
    {
        _currentColumn.Type = type;
        _currentColumn.Size = size;
        _currentColumn.Precision = precision;
        return this;
    }

    public IColumnCreator AsNotNull()
    {
        _currentColumn.IsNull = false;
        return this;
    }

    public IColumnCreator AsNull()
    {
        _currentColumn.IsNull = true;
        return this;
    }

    public IColumnCreator AsUnique(string name)
    {
        _currentColumn.UniqueExpression = new(name);
        return this;
    }

    public IColumnCreator AsDefault(string name, string defaultValue)
    {
        _currentColumn.DefaultExpression = new(name, defaultValue);
        return this;
    }

    public IColumnCreator AsPrimaryKey(string name)
    {
        _currentColumn.PrimaryKeyExpression = new(name);
        return this;
    }

    public IColumnCreator AsForeignKey(string name, string referenceTable, string referenceColumn)
    {
        _currentColumn.ForeignKeyExpression = new(name, referenceTable, referenceColumn);
        return this;
    }

    public IColumnCreator AsForeignKey(string name, string associatedTableName, string referenceTable,
        string associatedColumnName,
        string referenceColumn)
    {
        _currentColumn.ForeignKeyExpression = new(name, referenceTable, referenceColumn);
        return this;
    }

    public IColumnCreator HasIndex(string name)
    {
        _currentColumn.IndexExpression = new(name, _createTableExpression.Name, _currentColumn.Name);
        return this;
    }

    public async Task ExecuteAsync(int? timeout = null, CancellationToken cancellationToken = default)
    {
        // Handle unique indexes
        foreach (var column in _createTableExpression.ColumnExpressions.Where(c =>
                     c.UniqueExpression is not null && c.IndexExpression is not null))
        {
            column.UniqueExpression = null;
            column.IndexExpression!.IsUnique = true;
        }

        await _dvlSql.ExecuteSqlAsync(ToString(), timeout, cancellationToken);
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        var commandBuilder = new DvlSqlSchemaBuilder(builder);

        _createTableExpression.Accept(commandBuilder);

        string result = builder.ToString();

        Statics.ParametersCount = 0;

        return result;
    }
}