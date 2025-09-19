namespace DvlSql.Expressions;

//todo make this interface maybe
public abstract class DvlSqlExpressionWithParameters : DvlSqlExpression
{
    public List<DvlSqlParameter> Parameters { get; set; } = [];

    public DvlSqlExpressionWithParameters WithParameters(IEnumerable<DvlSqlParameter> @params)
    {
        Parameters = @params.ToList();
        return this;
    }
}