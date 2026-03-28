using System.Data;

namespace DvlSql;

public interface IColumnAlterer : ISchemaExecutable
{
    IColumnAlterer AsType(DvlSqlType type);
    IColumnAlterer AsType(SqlDbType type, int? size = null, byte? precision = null, byte? scale = null);
    IColumnAlterer AsNotNull();
    IColumnAlterer AsNull();
    IColumnAlterer AsUnique(string name);
    IColumnAlterer AsDefault(string name, string defaultValue);
    IColumnAlterer AsPrimaryKey(string name);
    IColumnAlterer AsForeignKey(string name, string referenceTable,  string referenceColumn);
    IColumnAlterer AsForeignKey(string name, string associatedTableName, string referenceTable,
        string associatedColumnName, string referenceColumn);
    IColumnAlterer HasIndex(string name);
}