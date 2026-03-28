namespace DvlSql.SqlServer;

public partial class DvlSqlMs
{
    public ITableCreator CreateTable(string tableName) => new DvlSqlTableCreator(tableName, this);

    public ITableCreator CreateTable(string associatedName, string tableName) => new DvlSqlTableCreator(tableName, this);

    public ITableAlterer AlterTable(string tableName) => throw new NotImplementedException();

    public ITableAlterer AlterTable(string associatedName, string tableName) => throw new NotImplementedException();

    public ISchemaExecutable RenameTable(string oldTableName, string newTableName) => throw new NotImplementedException();

    public ISchemaExecutable RenameTable(string associatedName, string oldTableName, string newTableName) => throw new NotImplementedException();

    public ISchemaExecutable DropTable(string tableName) => throw new NotImplementedException();

    public ISchemaExecutable DropTable(string associatedName, string tableName) => throw new NotImplementedException();
}