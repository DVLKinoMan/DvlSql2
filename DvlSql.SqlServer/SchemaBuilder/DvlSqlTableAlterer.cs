using System.Data;
using System.Text;
using DvlSql.Expressions;

namespace DvlSql.SqlServer;

public class DvlSqlTableAlterer : ITableAlterer, IColumnAlterer
{
    private readonly IDvlSql _dvlSql;
    private readonly DvlSqlAlterTableExpression _alterTableExpression;

    public DvlSqlTableAlterer(string tableName, IDvlSql dvlSql)
    {
        _dvlSql = dvlSql;
        _alterTableExpression = new(tableName);
    }

    public IColumnAlterer WithColumn(string name)
    {
        _alterTableExpression.AddColumnExpression = new(name);
        return this;
    }

    public IColumnAlterer WithColumn(string associatedName, string name)
    {
        _alterTableExpression.AddColumnExpression = new(name);
        return this;
    }

    public IColumnAlterer AlterColumn(string columnName)
    {
        _alterTableExpression.AlterColumnExpression = new(columnName);
        return this;
    }

    public IColumnAlterer AlterColumn(string associatedName, string columnName)
    {
        _alterTableExpression.AlterColumnExpression = new(columnName);
        return this;
    }

    public IColumnAlterer AsType(DvlSqlType type)
    {
        _alterTableExpression.AlterColumnExpression!.Type = type.SqlDbType;
        _alterTableExpression.AlterColumnExpression.Size = type.Size;
        _alterTableExpression.AlterColumnExpression.Precision = type.Precision;
        return this;
    }

    public IColumnAlterer AsType(SqlDbType type, int? size = null, byte? precision = null, byte? scale = null)
    {
        _alterTableExpression.AlterColumnExpression!.Type = type;
        _alterTableExpression.AlterColumnExpression.Size = size;
        _alterTableExpression.AlterColumnExpression.Precision = precision;
        return this;
    }

    public IColumnAlterer AsNotNull()
    {
        _alterTableExpression.AlterColumnExpression!.IsNull = false;
        return this;
    }

    public IColumnAlterer AsNull()
    {
        _alterTableExpression.AlterColumnExpression!.IsNull = true;
        return this;
    }

    public IColumnAlterer AsUnique(string name)
    {
        _alterTableExpression.AlterColumnExpression!.UniqueExpression =
            new(name, _alterTableExpression.AlterColumnExpression.Name);
        return this;
    }

    public IColumnAlterer AsDefault(string name, string defaultValue)
    {
        _alterTableExpression.AlterColumnExpression!.DefaultExpression =
            new(name, defaultValue, _alterTableExpression.AlterColumnExpression.Name);
        return this;
    }

    public IColumnAlterer AsPrimaryKey(string name)
    {
        _alterTableExpression.AlterColumnExpression!.PrimaryKeyExpression =
            new(name, _alterTableExpression.AlterColumnExpression.Name);
        return this;
    }

    public IColumnAlterer AsForeignKey(string name, string referenceTable, string referenceColumn)
    {
        _alterTableExpression.AlterColumnExpression!.ForeignKeyExpression = new(name,
            _alterTableExpression.AlterColumnExpression.Name, referenceTable, referenceColumn);
        return this;
    }

    public IColumnAlterer AsForeignKey(string name, string associatedTableName, string referenceTable,
        string associatedColumnName,
        string referenceColumn)
    {
        _alterTableExpression.AlterColumnExpression!.ForeignKeyExpression = new(name,
            _alterTableExpression.AlterColumnExpression.Name, referenceTable, referenceColumn);
        return this;
    }

    public IColumnAlterer HasIndex(string name)
    {
        _alterTableExpression.AlterColumnExpression!.IndexExpression = new(name, _alterTableExpression.Name,
            _alterTableExpression.AlterColumnExpression.Name);
        return this;
    }

    public async Task ExecuteAsync(int? timeout = null, CancellationToken cancellationToken = default)
    {
        // Handle unique indexes
        if (_alterTableExpression.AlterColumnExpression?.UniqueExpression is not null &&
            _alterTableExpression.AlterColumnExpression.IndexExpression is not null)
        {
            _alterTableExpression.AlterColumnExpression.UniqueExpression = null;
            _alterTableExpression.AlterColumnExpression.IndexExpression!.IsUnique = true;
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
        _alterTableExpression.DropColumnExpression = new(columnName);
        return this;
    }

    public ISchemaExecutable DropIndex(string name)
    {
        _alterTableExpression.DropIndexExpression = new(name);
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
        _alterTableExpression.RenameColumnExpression = new(oldColumnName, newColumnName);
        return this;
    }
}