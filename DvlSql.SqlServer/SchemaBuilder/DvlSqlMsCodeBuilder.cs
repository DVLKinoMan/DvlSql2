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
    
    public void Visit(DvlSqlAlterColumnExpression expression)
    {
        throw new NotImplementedException();
    }

    public void Visit(DvlSqlAlterTableExpression expression)
    {
        _builder.Append($$"""
                          await {{_dvlSqlVariableName}}
                                .AlterTable(nameof({{expression.AssociatedName}}), "{{expression.Name}}"))
                                .ExecuteAsync();
                          """);
        
        // drop column
        
        // drop index
        // drop constraint
        // rename column
        // add column
        // alter column
        
        
        dvl.AlterTable(expression.AssociatedName, expression.Name).DropColumn(LLdfasdf).
            .RenameTo(expression.NewName);
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public void Visit(DvlSqlCreateTableExpression expression)
    {
        throw new NotImplementedException();
    }

    public void Visit(DvlSqlCreateColumnExpression expression)
    {
        throw new NotImplementedException();
    }

    public void Visit(DvlSqlDefaultExpression expression)
    {
        throw new NotImplementedException();
    }

    public void Visit(DvlSqlForeignKeyExpression expression)
    {
        throw new NotImplementedException();
    }

    public void Visit(DvlSqlIndexExpression expression)
    {
        throw new NotImplementedException();
    }

    public void Visit(DvlSqlPrimaryKeyExpression expression)
    {
        throw new NotImplementedException();
    }

    public void Visit(DvlSqlUniqueExpression expression)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return _builder.ToString();
    }
}