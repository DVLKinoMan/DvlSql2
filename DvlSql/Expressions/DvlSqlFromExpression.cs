namespace DvlSql.Expressions;


public abstract class DvlSqlFromExpression : DvlSqlExpression
{
    public DvlSqlAsExpression? As { get; set; }
}

public class DvlSqlFromWithTableExpression : DvlSqlFromExpression
{
    public string TableName { get; init; }
    public bool WithNoLock { get; init; }

    public DvlSqlFromWithTableExpression(string tableName, bool withNoLock = false) =>
        (TableName, WithNoLock) = (tableName, withNoLock);

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone()
    {
        throw new NotImplementedException();
    }
}
