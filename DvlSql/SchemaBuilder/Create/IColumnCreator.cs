using System.Data;

namespace DvlSql;

public interface IColumnCreator : ITableCreator
{
   IColumnCreator AsType(DvlSqlType type);
   IColumnCreator AsType(SqlDbType type, int? size = null, byte? precision = null, byte? scale = null);
   IColumnCreator AsNotNull();
   IColumnCreator AsNull();
   IColumnCreator AsUnique(string name);
   IColumnCreator AsDefault(string name, string defaultValue);
   IColumnCreator AsPrimaryKey(string name);
   IColumnCreator AsForeignKey(string name, string referenceTable,  string referenceColumn);
   IColumnCreator AsForeignKey(string name, string associatedTableName, string referenceTable,
        string associatedColumnName, string referenceColumn);
   IColumnCreator HasIndex(string name);
}