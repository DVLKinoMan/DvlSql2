using System.Data;

namespace DvlSql;

public interface ITableCreator
{
    IColumnCreator WithColumn(string name);
    IColumnCreator WithColumn(string associatedName, string name);
}