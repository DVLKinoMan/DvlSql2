namespace DvlSql.Expressions;

public class DvlSqlAlterTableExpression(string name)
{
    public string Name { get; } = name;
    public DvlSqlAlterColumnExpression? AlterColumnExpression { get; set; }
    public DvlSqlCreateColumnExpression? AddColumnExpression { get; set; }
    public DvlSqlDropConstraintExpression? DropConstraintExpression { get; set; }
    public DvlSqlDropIndexExpression? DropIndexExpression { get; set; }
    public DvlSqlRenameColumnExpression? RenameColumnExpression { get; set; }
    public DvlSqlDropColumnExpression? DropColumnExpression { get; set; }
    
    public void Accept(IAlterTableVisitor visitor) => visitor.Visit(this);
}