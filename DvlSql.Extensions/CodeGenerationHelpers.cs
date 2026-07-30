using DvlSql.Expressions;

namespace DvlSql.Extensions;

public static class CodeGenerationHelpers
{
    public static string GetMigrationCode(
        this IEnumerable<DvlSqlCreateTableExpression> firstTableExpressions,
        IEnumerable<DvlSqlCreateTableExpression> secondTableExpressions,
        ICodeBuilder codeBuilder)
    {
        foreach (var migrationExpression in SchemaHelpers.GenerateMigrationExpressions(firstTableExpressions, secondTableExpressions))
            migrationExpression.Visit(codeBuilder);
        
        return codeBuilder.ToString();
    }
}