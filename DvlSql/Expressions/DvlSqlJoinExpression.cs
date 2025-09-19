namespace DvlSql.Expressions;

public abstract class DvlSqlJoinExpression(string tableName) : DvlSqlExpression
{
    public string TableName { get; init; } = tableName;
    public new bool IsRoot { get; init; } = true;
}

public abstract class DvlSqlGeneralJoinExpression(string tableName, DvlSqlComparisonExpression comp)
    : DvlSqlJoinExpression(tableName)
{
    public DvlSqlComparisonExpression ComparisonExpression { get; set; } = comp;

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
}

public abstract class DvlSqlJoinExpression<T>(string tableName, DvlSqlComparisonExpression<T> comp)
    : DvlSqlJoinExpression(tableName)
{
    public DvlSqlComparisonExpression<T> ComparisonExpression { get; set; } = comp;

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
}

public class DvlSqlFullJoinExpression<T>(string tableName, DvlSqlComparisonExpression<T> comparisonExpression)
    : DvlSqlJoinExpression<T>(tableName, comparisonExpression)
{
    public override DvlSqlExpression Clone()
    {
        throw new NotImplementedException();
    }
}

public class DvlSqlInnerJoinExpression<T>(string tableName, DvlSqlComparisonExpression<T> comparisonExpression)
    : DvlSqlJoinExpression<T>(tableName, comparisonExpression)
{
    public override DvlSqlExpression Clone()
    {
        throw new NotImplementedException();
    }
}

public class DvlSqlLeftJoinExpression<T>(string tableName, DvlSqlComparisonExpression<T> comparisonExpression)
    : DvlSqlJoinExpression<T>(tableName, comparisonExpression)
{
    public override DvlSqlExpression Clone()
    {
        throw new NotImplementedException();
    }
}

public class DvlSqlRightJoinExpression<T>(string tableName, DvlSqlComparisonExpression<T> comparisonExpression)
    : DvlSqlJoinExpression<T>(tableName, comparisonExpression)
{
    public override DvlSqlExpression Clone()
    {
        throw new NotImplementedException();
    }
}

public class DvlSqlFullJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression)
    : DvlSqlGeneralJoinExpression(tableName, comparisonExpression)
{
    public override DvlSqlExpression Clone()
    {
        throw new NotImplementedException();
    }
}

public class DvlSqlInnerJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression)
    : DvlSqlGeneralJoinExpression(tableName, comparisonExpression)
{
    public override DvlSqlExpression Clone()
    {
        throw new NotImplementedException();
    }
}

public class DvlSqlLeftJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression)
    : DvlSqlGeneralJoinExpression(tableName, comparisonExpression)
{
    public override DvlSqlExpression Clone()
    {
        throw new NotImplementedException();
    }
}

public class DvlSqlRightJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression)
    : DvlSqlGeneralJoinExpression(tableName, comparisonExpression)
{
    public override DvlSqlExpression Clone()
    {
        throw new NotImplementedException();
    }
}