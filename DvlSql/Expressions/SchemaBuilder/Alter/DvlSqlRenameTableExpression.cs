namespace DvlSql.Expressions;

public class DvlSqlRenameTableExpression(string oldTableName, string newTableName)
{
    public string OldTableName { get; } = oldTableName;
    public string NewTableName { get; } = newTableName;
    
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
}