namespace DvlSql;

public interface ITableAlterer
{
    IColumnAlterer WithColumn(string name);
    IColumnAlterer WithColumn(string associatedName, string name);
    IColumnAlterer AlterColumn(string columnName);
    IColumnAlterer AlterColumn(string associatedName, string columnName);
    ISchemaExecutable DropColumn(string columnName);
    ISchemaExecutable DropColumn(string associatedName, string columnName);
    ISchemaExecutable DropIndex(string name);
    ISchemaExecutable DropIndex(string associatedName, string name);
    ISchemaExecutable DropConstraint(string name);
    ISchemaExecutable RenameColumn(string oldColumnName, string newColumnName);
    ISchemaExecutable RenameColumn(string associatedName, string oldColumnName, string newColumnName);
}