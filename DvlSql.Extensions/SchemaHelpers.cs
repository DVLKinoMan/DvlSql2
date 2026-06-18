using DvlSql.Expressions;

namespace DvlSql.Extensions;

public static class SchemaHelpers
{
    public static IEnumerable<DvlSqlSchemaExpression> GenerateMigrationExpressions(
        this IEnumerable<DvlSqlCreateTableExpression> firstTableExpressions,
        IEnumerable<DvlSqlCreateTableExpression> secondTableExpressions)
    {
        var tablesFromFirstDict = firstTableExpressions.ToDictionary(c => c.AssociatedName ?? c.Name);
        var tablesFromSecondDict = secondTableExpressions.ToDictionary(c => c.AssociatedName ?? c.Name);

        foreach (var (associatedName, firstTableExpression) in tablesFromFirstDict)
        {
            if (tablesFromSecondDict.TryGetValue(associatedName, out var secondTableExpression))
                foreach (var migrationExpression in GenerateMigrationExpressions(firstTableExpression, secondTableExpression))
                    yield return migrationExpression;
            else yield return new DvlSqlDropTableExpression(firstTableExpression.Name, firstTableExpression.AssociatedName);
        }
        
        foreach (var (associatedName, secondTableExpression) in tablesFromSecondDict)
        {
            if (tablesFromFirstDict.ContainsKey(associatedName))
                continue;

            yield return secondTableExpression;
        }
    }

    public static IEnumerable<DvlSqlSchemaExpression> GenerateMigrationExpressions(
        this DvlSqlCreateTableExpression firstTableExpression,
        DvlSqlCreateTableExpression secondTableExpression)
    {
        if (firstTableExpression.AssociatedName != secondTableExpression.AssociatedName)
            yield break;

        if (firstTableExpression.Name != secondTableExpression.Name)
            yield return new DvlSqlRenameTableExpression(firstTableExpression.Name, secondTableExpression.Name, firstTableExpression.AssociatedName);

        var columnsFromFirstTableDict = firstTableExpression.ColumnExpressions.ToDictionary(c => c.AssociatedName ?? c.Name);
        var columnsFromSecondTableDict = secondTableExpression.ColumnExpressions.ToDictionary(c => c.AssociatedName ?? c.Name);

        foreach (var (associatedName, firstColumnExpression) in columnsFromFirstTableDict)
        {
            if (columnsFromSecondTableDict.TryGetValue(associatedName, out var secondColumnExpression))
                foreach (var migrationExpression in GenerateMigrationExpressions(firstColumnExpression, secondColumnExpression))
                    yield return migrationExpression;
            else yield return new DvlSqlDropTableExpression(firstColumnExpression.Name, firstColumnExpression.AssociatedName);
        }

        foreach (var (associatedName, secondColumnExpression) in columnsFromSecondTableDict)
        {
            if (columnsFromFirstTableDict.ContainsKey(associatedName))
                continue;

            yield return secondColumnExpression;
        }
    }

    private static IEnumerable<DvlSqlSchemaExpression> GenerateMigrationExpressions(
        this DvlSqlCreateColumnExpression columnExpression,
        DvlSqlCreateColumnExpression modifiedColumnExpression)
    {
        if (columnExpression.AssociatedName != modifiedColumnExpression.AssociatedName)
            yield break;

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

        foreach (var expression in GenerateMigrationExpressions(columnExpression.PrimaryKeyExpression, modifiedColumnExpression.PrimaryKeyExpression,
                     GetAlterColumnExpression))
            yield return expression;

        foreach (var expression in GenerateMigrationExpressions(columnExpression.DefaultExpression, modifiedColumnExpression.DefaultExpression,
                     GetAlterColumnExpression))
            yield return expression;

        foreach (var expression in GenerateMigrationExpressions(columnExpression.ForeignKeyExpression, modifiedColumnExpression.ForeignKeyExpression,
                     GetAlterColumnExpression))
            yield return expression;

        foreach (var expression in GenerateMigrationExpressions(columnExpression.IndexExpression, modifiedColumnExpression.IndexExpression,
                     GetAlterColumnExpression))
            yield return expression;

        foreach (var expression in GenerateMigrationExpressions(columnExpression.UniqueExpression, modifiedColumnExpression.UniqueExpression,
                     GetAlterColumnExpression))
            yield return expression;

        DvlSqlAlterColumnExpression GetAlterColumnExpression()
            => alterColumnExpression ??= new DvlSqlAlterColumnExpression(modifiedColumnExpression.Name, columnExpression.AssociatedName);
    }

    private static IEnumerable<DvlSqlSchemaExpression> GenerateMigrationExpressions(
        this DvlSqlPrimaryKeyExpression? primaryExpression,
        DvlSqlPrimaryKeyExpression? modifiedPrimaryExpression,
        Func<DvlSqlAlterColumnExpression> alterColumnExpressionFunc)
    {
        if (primaryExpression is null && modifiedPrimaryExpression is null)
            yield break;

        if (primaryExpression is null)
        {
            alterColumnExpressionFunc().PrimaryKeyExpression = modifiedPrimaryExpression;
            yield break;
        }

        if (modifiedPrimaryExpression is null)
        {
            yield return new DvlSqlDropConstraintExpression(primaryExpression.Name);
            yield break;
        }

        if (primaryExpression != modifiedPrimaryExpression)
        {
            yield return new DvlSqlDropConstraintExpression(primaryExpression.Name);
            alterColumnExpressionFunc().PrimaryKeyExpression = modifiedPrimaryExpression;
        }
    }

    private static IEnumerable<DvlSqlSchemaExpression> GenerateMigrationExpressions(
        this DvlSqlForeignKeyExpression? foreignKeyExpression,
        DvlSqlForeignKeyExpression? modifiedForeignKeyExpression,
        Func<DvlSqlAlterColumnExpression> alterColumnExpressionFunc)
    {
        if (foreignKeyExpression is null && modifiedForeignKeyExpression is null)
            yield break;

        if (foreignKeyExpression is null)
        {
            alterColumnExpressionFunc().ForeignKeyExpression = modifiedForeignKeyExpression;
            yield break;
        }

        if (modifiedForeignKeyExpression is null)
        {
            yield return new DvlSqlDropConstraintExpression(foreignKeyExpression.Name);
            yield break;
        }

        if (foreignKeyExpression != modifiedForeignKeyExpression)
        {
            yield return new DvlSqlDropConstraintExpression(foreignKeyExpression.Name);
            alterColumnExpressionFunc().ForeignKeyExpression = modifiedForeignKeyExpression;
        }
    }

    private static IEnumerable<DvlSqlSchemaExpression> GenerateMigrationExpressions(
        this DvlSqlDefaultExpression? defaultExpression,
        DvlSqlDefaultExpression? modifiedDefaultExpression,
        Func<DvlSqlAlterColumnExpression> alterColumnExpressionFunc)
    {
        if (defaultExpression is null && modifiedDefaultExpression is null)
            yield break;

        if (defaultExpression is null)
        {
            alterColumnExpressionFunc().DefaultExpression = modifiedDefaultExpression;
            yield break;
        }

        if (modifiedDefaultExpression is null)
        {
            yield return new DvlSqlDropConstraintExpression(defaultExpression.Name);
            yield break;
        }

        if (defaultExpression != modifiedDefaultExpression)
        {
            yield return new DvlSqlDropConstraintExpression(defaultExpression.Name);
            alterColumnExpressionFunc().DefaultExpression = modifiedDefaultExpression;
        }
    }

    private static IEnumerable<DvlSqlSchemaExpression> GenerateMigrationExpressions(
        this DvlSqlIndexExpression? indexExpression,
        DvlSqlIndexExpression? modifiedIndexExpression,
        Func<DvlSqlAlterColumnExpression> alterColumnExpressionFunc)
    {
        if (indexExpression == null && modifiedIndexExpression == null)
            yield break;

        if (indexExpression == null)
        {
            alterColumnExpressionFunc().IndexExpression = modifiedIndexExpression;
            yield break;
        }

        if (modifiedIndexExpression == null)
        {
            yield return new DvlSqlDropConstraintExpression(indexExpression.Name);
            yield break;
        }

        if (indexExpression != modifiedIndexExpression)
        {
            yield return new DvlSqlDropConstraintExpression(indexExpression.Name);
            alterColumnExpressionFunc().IndexExpression = modifiedIndexExpression;
        }
    }

    private static IEnumerable<DvlSqlSchemaExpression> GenerateMigrationExpressions(
        this DvlSqlUniqueExpression? uniqueExpression,
        DvlSqlUniqueExpression? modifiedUniqueExpression,
        Func<DvlSqlAlterColumnExpression> alterColumnExpressionFunc)
    {
        if (uniqueExpression == null && modifiedUniqueExpression == null)
            yield break;

        if (uniqueExpression == null)
        {
            alterColumnExpressionFunc().UniqueExpression = modifiedUniqueExpression;
            yield break;
        }

        if (modifiedUniqueExpression == null)
        {
            yield return new DvlSqlDropConstraintExpression(uniqueExpression.Name);
            yield break;
        }

        if (uniqueExpression != modifiedUniqueExpression)
        {
            yield return new DvlSqlDropConstraintExpression(uniqueExpression.Name);
            alterColumnExpressionFunc().UniqueExpression = modifiedUniqueExpression;
        }
    }
}