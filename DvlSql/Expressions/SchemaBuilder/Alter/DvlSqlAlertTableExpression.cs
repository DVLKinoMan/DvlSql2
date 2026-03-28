namespace DvlSql.Expressions;

public class DvlSqlAlertTableExpression(string name)
{
    public string Name { get; } = name;
    public DvlSqlColumnExpression? ColumnExpression { get; set; }
    public DvlSqlDropConstraintExpression? DropConstraintExpression { get; set; }
    public DvlSqlDropIndexExpression? DropIndexExpression { get; set; }
}