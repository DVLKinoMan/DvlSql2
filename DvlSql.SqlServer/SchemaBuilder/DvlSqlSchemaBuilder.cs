using System.Text;
using DvlSql.Expressions;

namespace DvlSql.SqlServer;

public class DvlSqlSchemaBuilder(StringBuilder command) : ISchemaBuilderVisitor
{
    private readonly StringBuilder _command = command;
    
    public void Visit(DvlSqlCreateTableExpression expression)
    {
        this._command.AppendLine($"CREATE TABLE {expression.Name}");
        this._command.AppendLine("(");
        foreach (var columnExpression in expression.ColumnExpressions)
        {
            columnExpression.Accept(this);
            this._command.Append(',');
            this._command.Append(Environment.NewLine);
        }
        this._command.Remove(_command.Length - 1, 1);
        this._command.AppendLine(");");
        
        // Handle indexes separately (SQL Server style)
        foreach (var column in expression.ColumnExpressions)
            column.IndexExpression?.Accept(this);
    }

    public void Visit(DvlSqlColumnExpression expression)
    {
        this._command.Append($"{expression.Name} ");

        this._command.Append(
            Exts.ToSqlString(
                expression.Type!.Value,
                expression.Size,
                expression.Precision,
                expression.Scale));

        this._command.Append(expression.IsNull ? " NULL" : " NOT NULL");

        // DEFAULT
        expression.DefaultExpression?.Accept(this);

        // PRIMARY KEY (inline style)
        expression.PrimaryKeyExpression?.Accept(this);

        // UNIQUE
        expression.UniqueExpression?.Accept(this);

        // FOREIGN KEY (inline reference)
        expression.ForeignKeyExpression?.Accept(this);
    }

    public void Visit(DvlSqlPrimaryKeyExpression expression)
    {
        _command.Append($" CONSTRAINT {expression.Name} PRIMARY KEY");
    }

    public void Visit(DvlSqlDefaultExpression expression)
    {
        _command.Append($" CONSTRAINT {expression.Name} DEFAULT {expression.Value}");
    }

    public void Visit(DvlSqlUniqueExpression expression)
    {
        _command.Append($" CONSTRAINT {expression.Name} UNIQUE");
    }

    public void Visit(DvlSqlForeignKeyExpression expression)
    {
        _command.Append($" CONSTRAINT {expression.Name} FOREIGN KEY REFERENCES {expression.ReferenceTableName}({expression.ReferenceColumnName})");
    }

    public void Visit(DvlSqlIndexExpression expression)
    {
        _command.AppendLine(
            $"CREATE {(expression.IsUnique ? "UNIQUE " : "")}INDEX {expression.Name} ON {expression.TableName}({expression.ColumnName});");
    }
}