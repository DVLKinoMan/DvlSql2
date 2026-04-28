using System.Data;
using System.Text;
using DvlSql.Expressions;

namespace DvlSql.SqlServer;

public class DvlSqlTableAlterer : ITableAlterer, IColumnAlterer
{
    private readonly IDvlSql _dvlSql;
    private readonly DvlSqlAlterTableExpression _alterTableExpression;

    private DvlSqlColumnExpression GetColumnExpression()
        => _alterTableExpression.AlterColumnExpression is not null
            ? _alterTableExpression.AlterColumnExpression
            : _alterTableExpression.AddColumnExpression
              ?? throw new InvalidOperationException("Column alter and add expressions are null");

    public DvlSqlTableAlterer(string tableName, IDvlSql dvlSql)
    {
        _dvlSql = dvlSql;
        _alterTableExpression = new(tableName);
    }
    
    public DvlSqlTableAlterer(string tableName, string associatedName, IDvlSql dvlSql)
    {
        _dvlSql = dvlSql;
        _alterTableExpression = new(tableName, associatedName);
    }

    public IColumnAlterer WithColumn(string name)
    {
        _alterTableExpression.AddColumnExpression = new(name);
        return this;
    }

    public IColumnAlterer WithColumn(string associatedName, string name)
    {
        _alterTableExpression.AddColumnExpression = new(name, associatedName);
        return this;
    }

    public IColumnAlterer AlterColumn(string columnName)
    {
        _alterTableExpression.AlterColumnExpression = new(columnName);
        return this;
    }

    public IColumnAlterer AlterColumn(string associatedName, string columnName)
    {
        _alterTableExpression.AlterColumnExpression = new(columnName, associatedName);
        return this;
    }

    public IColumnAlterer AsType(DvlSqlType type)
    {
        var columnExp = GetColumnExpression();
        columnExp.Type = type.SqlDbType;
        columnExp.Size = type.Size;
        columnExp.Precision = type.Precision;
        return this;
    }

    public IColumnAlterer AsType(SqlDbType type, int? size = null, byte? precision = null, byte? scale = null)
    {
        var columnExp = GetColumnExpression();
        columnExp.Type = type;
        columnExp.Size = size;
        columnExp.Precision = precision;
        return this;
    }

    public IColumnAlterer AsNotNull()
    {
        var columnExp = GetColumnExpression();
        columnExp.IsNull = false;
        return this;
    }

    public IColumnAlterer AsNull()
    {
        var columnExp = GetColumnExpression();
        columnExp.IsNull = true;
        return this;
    }

    public IColumnAlterer AsUnique(string name)
    {
        var columnExp = GetColumnExpression();
        columnExp.UniqueExpression = new(name, columnExp.Name);
        return this;
    }

    public IColumnAlterer AsDefault(string name, string defaultValue)
    {
        var columnExp = GetColumnExpression();
        columnExp.DefaultExpression = new(name, defaultValue, columnExp.Name);
        return this;
    }

    public IColumnAlterer AsPrimaryKey(string name)
    {
        var columnExp = GetColumnExpression();
        columnExp.PrimaryKeyExpression = new(name, columnExp.Name);
        return this;
    }

    public IColumnAlterer AsForeignKey(string name, string referenceTable, string referenceColumn)
    {
        var columnExp = GetColumnExpression();
        columnExp.ForeignKeyExpression = new(name, columnExp.Name, referenceTable, referenceColumn);
        return this;
    }

    public IColumnAlterer AsForeignKey(string name, string associatedTableName, string referenceTable,
        string associatedColumnName,
        string referenceColumn)
    {
        var columnExp = GetColumnExpression();
        columnExp.ForeignKeyExpression = new(name, columnExp.Name, referenceTable, referenceColumn);
        return this;
    }

    public IColumnAlterer HasIndex(string name)
    {
        var columnExp = GetColumnExpression();
        columnExp.IndexExpression = new(name, _alterTableExpression.Name, columnExp.Name);
        return this;
    }

    public async Task ExecuteAsync(int? timeout = null, CancellationToken cancellationToken = default)
    {
        // Handle unique indexes
        var columnExp = GetColumnExpression();
        if (columnExp?.UniqueExpression is not null &&
            columnExp.IndexExpression is not null)
        {
            columnExp.UniqueExpression = null;
            columnExp.IndexExpression!.IsUnique = true;
        }

        await _dvlSql.ExecuteSqlAsync(ToString(), timeout, cancellationToken);
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        var commandBuilder = new DvlSqlAlterTableBuilder(builder);

        _alterTableExpression.Accept(commandBuilder);

        string result = builder.ToString();

        Statics.ParametersCount = 0;

        return result;
    }

    public ISchemaExecutable DropColumn(string columnName)
    {
        _alterTableExpression.DropColumnExpression = new(columnName);
        return this;
    }

    public ISchemaExecutable DropColumn(string associatedName, string columnName)
    {
        _alterTableExpression.DropColumnExpression = new(columnName, associatedName);
        return this;
    }

    public ISchemaExecutable DropIndex(string name)
    {
        _alterTableExpression.DropIndexExpression = new(name);
        return this;
    }

    public ISchemaExecutable DropIndex(string associatedName, string name)
    {
        _alterTableExpression.DropIndexExpression = new(name, associatedName);
        return this;
    }

    public ISchemaExecutable DropConstraint(string name)
    {
        _alterTableExpression.DropConstraintExpression = new(name);
        return this;
    }

    public ISchemaExecutable RenameColumn(string oldColumnName, string newColumnName)
    {
        _alterTableExpression.RenameColumnExpression = new(oldColumnName, newColumnName);
        return this;
    }

    public ISchemaExecutable RenameColumn(string associatedName, string oldColumnName, string newColumnName)
    {
        _alterTableExpression.RenameColumnExpression = new(oldColumnName, newColumnName, associatedName);
        return this;
    }
}