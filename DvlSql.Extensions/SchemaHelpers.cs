using DvlSql.Expressions;

namespace DvlSql.Extensions;

public static class SchemaHelpers
{
    public static List<DvlSqlSchemaExpression> GenerateMigrationExpressions(
        this DvlSqlCreateTableExpression firstTableExpression,
        DvlSqlCreateTableExpression secondTableExpression)
    {
    }

    private static IEnumerable<DvlSqlSchemaExpression> GenerateMigrationExpressions(
        this DvlSqlCreateColumnExpression columnExpression,
        DvlSqlCreateColumnExpression modifiedColumnExpression)
    {
        DvlSqlAlterColumnExpression? alterColumnExpression = null;
        if (columnExpression.Name != modifiedColumnExpression.Name)
            yield return new DvlSqlRenameColumnExpression(columnExpression.Name, modifiedColumnExpression.Name, columnExpression.AssociatedName);

        if (columnExpression.Type != modifiedColumnExpression.Type ||
            columnExpression.Size != modifiedColumnExpression.Size ||
            columnExpression.Precision != modifiedColumnExpression.Precision ||
            columnExpression.Scale != modifiedColumnExpression.Scale ||
            columnExpression.IsNull != modifiedColumnExpression.IsNull)
            alterColumnExpression =
                new DvlSqlAlterColumnExpression(modifiedColumnExpression.Name, columnExpression.AssociatedName)
                {
                    Type = modifiedColumnExpression.Type,
                    Size = modifiedColumnExpression.Size,
                    Precision = modifiedColumnExpression.Precision,
                    Scale = modifiedColumnExpression.Scale,
                    IsNull = modifiedColumnExpression.IsNull
                };
        
        foreach (var expression in GenerateMigrationExpressions(columnExpression.DefaultExpression, modifiedColumnExpression.DefaultExpression, GetAlterColumnExpression))
            yield return expression;
        
        foreach (var expression in GenerateMigrationExpressions(columnExpression.PrimaryKeyExpression, modifiedColumnExpression.PrimaryKeyExpression, GetAlterColumnExpression))
            yield return expression;

        DvlSqlAlterColumnExpression GetAlterColumnExpression()
            => alterColumnExpression ??= new DvlSqlAlterColumnExpression(modifiedColumnExpression.Name, columnExpression.AssociatedName);
    }
    
    private static IEnumerable<DvlSqlSchemaExpression> GenerateMigrationExpressions(
        this DvlSqlPrimaryKeyExpression? primaryExpression,
        DvlSqlPrimaryKeyExpression? modifiedPrimaryExpression,
        Func<DvlSqlAlterColumnExpression> alterColumnExpressionFunc)
    {
        if(primaryExpression == null && modifiedPrimaryExpression == null)
            yield break;
        
        if (primaryExpression == null)
        {
            alterColumnExpressionFunc().PrimaryKeyExpression = modifiedPrimaryExpression;
            yield break;
        }

        if (modifiedPrimaryExpression == null)
        {
            yield return new DvlSqlDropConstraintExpression(primaryExpression.Name);
            yield break;
        }

        if (primaryExpression.Name != modifiedPrimaryExpression.Name ||
            primaryExpression.ColumnName != modifiedPrimaryExpression.ColumnName)
        {
            yield return new DvlSqlDropConstraintExpression(primaryExpression.Name);
            alterColumnExpressionFunc().PrimaryKeyExpression = modifiedPrimaryExpression;
        }
    }

    private static IEnumerable<DvlSqlSchemaExpression> GenerateMigrationExpressions(
        this DvlSqlDefaultExpression? defaultExpression,
        DvlSqlDefaultExpression? modifiedDefaultExpression,
        Func<DvlSqlAlterColumnExpression> alterColumnExpressionFunc)
    {
        if(defaultExpression == null && modifiedDefaultExpression == null)
            yield break;
        
        if (defaultExpression == null)
        {
            alterColumnExpressionFunc().DefaultExpression = modifiedDefaultExpression;
            yield break;
        }

        if (modifiedDefaultExpression == null)
        {
            yield return new DvlSqlDropConstraintExpression(defaultExpression.Name);
            yield break;
        }

        if (defaultExpression.Name != modifiedDefaultExpression.Name ||
            defaultExpression.Value != modifiedDefaultExpression.Value)
        {
            yield return new DvlSqlDropConstraintExpression(defaultExpression.Name);
            alterColumnExpressionFunc().DefaultExpression = modifiedDefaultExpression;
        }
    }
}