using System.Data;
using System.Text;
using DvlSql.Expressions;

namespace DvlSql.SqlServer;

internal class DvlSqlTableCreator : ITableCreator, IColumnCreator
{
    private readonly IDvlSql _dvlSql;
    private readonly DvlSqlCreateTableExpression _createTableExpression;
    private DvlSqlCreateColumnExpression _currentCreateColumn;

    public DvlSqlTableCreator(string tableName, IDvlSql dvlSql)
    {
        _dvlSql = dvlSql;
        _createTableExpression = new(tableName);
    }

    public IColumnCreator WithColumn(string name)
    {
        _currentCreateColumn = new(name);
        _createTableExpression.ColumnExpressions.Add(_currentCreateColumn);
        return this;
    }

    public IColumnCreator WithColumn(string associatedName, string name)
    {
        _currentCreateColumn = new(name);
        _createTableExpression.ColumnExpressions.Add(_currentCreateColumn);
        return this;
    }

    public IColumnCreator AsType(DvlSqlType type)
    {
        _currentCreateColumn.Type = type.SqlDbType;
        _currentCreateColumn.Size = type.Size;
        _currentCreateColumn.Precision = type.Precision;
        return this;
    }

    public IColumnCreator AsType(SqlDbType type, int? size = null, byte? precision = null, byte? scale = null)
    {
        _currentCreateColumn.Type = type;
        _currentCreateColumn.Size = size;
        _currentCreateColumn.Precision = precision;
        return this;
    }

    public IColumnCreator AsNotNull()
    {
        _currentCreateColumn.IsNull = false;
        return this;
    }

    public IColumnCreator AsNull()
    {
        _currentCreateColumn.IsNull = true;
        return this;
    }

    public IColumnCreator AsUnique(string name)
    {
        _currentCreateColumn.UniqueExpression = new(name, _currentCreateColumn.Name);
        return this;
    }

    public IColumnCreator AsDefault(string name, string defaultValue)
    {
        _currentCreateColumn.DefaultExpression = new(name, defaultValue, _currentCreateColumn.Name);
        return this;
    }

    public IColumnCreator AsPrimaryKey(string name)
    {
        _currentCreateColumn.PrimaryKeyExpression = new(name, _currentCreateColumn.Name);
        return this;
    }

    public IColumnCreator AsForeignKey(string name, string referenceTable, string referenceColumn)
    {
        _currentCreateColumn.ForeignKeyExpression = new(name, _currentCreateColumn.Name, referenceTable, referenceColumn);
        return this;
    }

    public IColumnCreator AsForeignKey(string name, string associatedTableName, string referenceTable,
        string associatedColumnName,
        string referenceColumn)
    {
        _currentCreateColumn.ForeignKeyExpression = new(name, _currentCreateColumn.Name, referenceTable, referenceColumn);
        return this;
    }

    public IColumnCreator HasIndex(string name)
    {
        _currentCreateColumn.IndexExpression = new(name, _createTableExpression.Name, _currentCreateColumn.Name);
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
        var commandBuilder = new DvlSqlCreateTableBuilder(builder);

        _createTableExpression.Accept(commandBuilder);

        string result = builder.ToString();

        Statics.ParametersCount = 0;

        return result;
    }
}