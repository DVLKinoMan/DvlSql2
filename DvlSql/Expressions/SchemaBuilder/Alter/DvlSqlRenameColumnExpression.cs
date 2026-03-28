namespace DvlSql.Expressions;

public class DvlSqlRenameColumnExpression(string oldColumnName, string newColumnName)
{
    public string OldColumnName { get; } = oldColumnName;
    public string NewColumnName { get; } = newColumnName;
    
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
}