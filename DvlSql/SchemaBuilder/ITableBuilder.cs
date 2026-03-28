namespace DvlSql;

public interface ITableBuilder
{
    ITableCreator CreateTable(string tableName);
    ITableCreator CreateTable(string associatedName, string tableName);
    ITableAlterer AlterTable(string tableName);
    ITableAlterer AlterTable(string associatedName, string tableName);
    ISchemaExecutable RenameTable(string oldTableName, string newTableName);
    ISchemaExecutable RenameTable(string associatedName, string oldTableName, string newTableName);
    ISchemaExecutable DropTable(string tableName);
    ISchemaExecutable DropTable(string associatedName, string tableName);
}