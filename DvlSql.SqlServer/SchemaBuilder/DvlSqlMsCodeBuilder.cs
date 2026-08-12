using System.Text;
using DvlSql.Expressions;

namespace DvlSql.SqlServer;

public class DvlSqlMsCodeBuilder : ICodeBuilder
{
    private StringBuilder _builder = new();
    private readonly string _dvlSqlVariableName;
    private IDvlSql dvl;

    public DvlSqlMsCodeBuilder(string? variableName = null)
    {
        _dvlSqlVariableName = variableName ?? "_sql";
    }

    public void Visit(DvlSqlAlterTableExpression expression)
    {
        _builder.Append($$"""
                          await {{_dvlSqlVariableName}}
                                .AlterTable(nameof({{expression.AssociatedName}}), "{{expression.Name}}"))
                                .ExecuteAsync();
                          """);
        
        // Ordering is not needed because probably only one expression will not be null
        if(expression.DropColumnExpression is { } dropColumnExpression)
            Visit(AlterTableFunc, dropColumnExpression);
        if(expression.DropIndexExpression is { } dropIndexExpression)
            Visit(AlterTableFunc, dropIndexExpression);
        if(expression.DropConstraintExpression is { } dropConstraintExpression)
            Visit(AlterTableFunc, dropConstraintExpression);
        if(expression.RenameColumnExpression is { } renameColumnExpression)
            Visit(AlterTableFunc, renameColumnExpression);
        if(expression.AddColumnExpression is { } addColumnExpression)
            Visit(addColumnExpression, true, AlterTableFunc);
        if(expression.AlterColumnExpression is { } alterColumnExpression)
            Visit(AlterTableFunc, alterColumnExpression);

        string AlterTableFunc() =>
            $$"""
              await {{_dvlSqlVariableName}}
                    .AlterTable(nameof({{expression.AssociatedName}}), "{{expression.Name}}"))
              """;
    }

    private void Visit(Func<string> alterTableFunc, DvlSqlDropColumnExpression expression)
    {
        _builder.Append(alterTableFunc());
        _builder.Append($$"""
                          .DropColumn(nameof({{expression.AssociatedName}}), "{{expression.Name}}"))
                          .ExecuteAsync();
                          """);
    }

    private void Visit(Func<string> alterTableFunc, DvlSqlDropIndexExpression expression)
    {
        _builder.Append(alterTableFunc());
        _builder.Append($$"""
                          .DropIndex(nameof({{expression.AssociatedName}}), "{{expression.Name}}"))
                          .ExecuteAsync();
                          """);
    }

    private void Visit(Func<string> alterTableFunc, DvlSqlDropConstraintExpression expression)
    {
        _builder.Append(alterTableFunc());
        _builder.Append($$"""
                          .DropConstraint(nameof({{expression.AssociatedName}}), "{{expression.Name}}"))
                          .ExecuteAsync();
                          """);
    }

    private void Visit(Func<string> alterTableFunc, DvlSqlRenameColumnExpression expression)
    {
        _builder.Append(alterTableFunc());
        _builder.Append($$"""
                          .RenameColumn(nameof({{expression.AssociatedName}}), "{{expression.OldColumnName}}", "{{expression.NewColumnName}}"))
                          .ExecuteAsync();
                          """);
    }

    private void Visit(Func<string> alterTableFunc, DvlSqlAlterColumnExpression expression)
    {
        _builder.Append(alterTableFunc());
        _builder.Append($$"""
                          .AlterColumn(nameof({{expression.AssociatedName}}), "{{expression.Name}}"))
                            .AsType({{expression.Type}}, {{expression.Size}}, {{expression.Precision}}, {{expression.Scale}})
                            .{{(expression.IsNull ? "AsNull" : "AsNotNull")}}()
                          """);
        
        if (expression.IndexExpression is { } indexExpression)
            Visit(indexExpression);
        if (expression.PrimaryKeyExpression is { } primaryKeyExpression)
            Visit(primaryKeyExpression);
        if (expression.UniqueExpression is { } uniqueExpression)
            Visit(uniqueExpression);
        if (expression.ForeignKeyExpression is { } foreignKeyExpression)
            Visit(foreignKeyExpression);
        if (expression.DefaultExpression is { } defaultExpression)
            Visit(defaultExpression);
        
        _builder.Append($$"""
                          .ExecuteAsync();
                          """);
    }

    private void Visit(DvlSqlCreateColumnExpression expression, bool withExecute, Func<string>? alterTableFunc = null)
    {
        if(alterTableFunc is not null)
            _builder.Append(alterTableFunc());
        
        _builder.Append($$"""
                          .WithColumn(nameof({{expression.AssociatedName}}), "{{expression.Name}}"))
                            .AsType({{expression.Type}}, {{expression.Size}}, {{expression.Precision}}, {{expression.Scale}})
                            .{{(expression.IsNull ? "AsNull" : "AsNotNull")}}()
                          """);
        
        if (expression.IndexExpression is { } indexExpression)
            Visit(indexExpression);
        if (expression.PrimaryKeyExpression is { } primaryKeyExpression)
            Visit(primaryKeyExpression);
        if (expression.UniqueExpression is { } uniqueExpression)
            Visit(uniqueExpression);
        if (expression.ForeignKeyExpression is { } foreignKeyExpression)
            Visit(foreignKeyExpression);
        if (expression.DefaultExpression is { } defaultExpression)
            Visit(defaultExpression);
        
        if(withExecute)
            _builder.Append($$"""
                              .ExecuteAsync();
                              """);
    }

    public void Visit(DvlSqlDropTableExpression expression)
    {
        _builder.Append($$"""
                          await {{_dvlSqlVariableName}}
                                .DropTable(nameof({{expression.AssociatedName}}), "{{expression.Name}}"))
                                .ExecuteAsync();
                          """);
    }

    public void Visit(DvlSqlRenameTableExpression expression)
    {
        _builder.Append($$"""
                          await {{_dvlSqlVariableName}}
                                .RenameTable(nameof({{expression.AssociatedName}}), "{{expression.OldTableName}}", "{{expression.NewTableName}}"))
                                .ExecuteAsync();
                          """);
    }

    public void Visit(DvlSqlCreateTableExpression expression)
    {
        _builder.Append($$"""
                          await {{_dvlSqlVariableName}}
                                .CreateTable(nameof({{expression.AssociatedName}}), "{{expression.Name}}"))
                          """);
        
        foreach(var columnExpression in expression.ColumnExpressions)
            Visit(columnExpression, false);
        
        _builder.Append($$"""
                          .ExecuteAsync();
                          """);
    }

    private void Visit(DvlSqlDefaultExpression expression)
    {
        _builder.Append($$"""
                          .AsDefault("{{expression.Name}}", "{{expression.Value}}")))
                          """);
    }

    private void Visit(DvlSqlForeignKeyExpression expression)
    {
        _builder.Append($$"""
                          .AsForeignKey("{{expression.Name}}", "{{expression.ReferenceTableName}}"), "{{expression.ReferenceColumnName}}")))
                          """);
    }

    private void Visit(DvlSqlIndexExpression expression)
    {
        _builder.Append($$"""
                          .HasIndex("{{expression.Name}}"))
                          """);
    }

    private void Visit(DvlSqlPrimaryKeyExpression expression)
    {
        _builder.Append($$"""
                          .AsPrimaryKey("{{expression.Name}}")))
                          """);
    }

    private void Visit(DvlSqlUniqueExpression expression)
    {
        _builder.Append($$"""
                          .AsUnique("{{expression.Name}}")))
                          """);
    }

    public override string ToString()
    {
        return _builder.ToString();
    }
}