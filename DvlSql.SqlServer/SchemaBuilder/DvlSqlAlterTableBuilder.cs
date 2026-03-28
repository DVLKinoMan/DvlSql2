using System.Text;
using DvlSql.Expressions;

namespace DvlSql.SqlServer;

internal class DvlSqlAlterTableBuilder(StringBuilder command) : IAlterTableVisitor
{
    private readonly StringBuilder _command = command;
    private string? _currentTable;

    public void Visit(DvlSqlAlterTableExpression expression)
    {
        _currentTable = expression.Name;

        // Drop index
        expression.RenameColumnExpression?.Accept(this);

        // Drop constraint
        expression.DropConstraintExpression?.Accept(this);

        // Drop index
        expression.DropIndexExpression?.Accept(this);

        // Drop column
        expression.DropColumnExpression?.Accept(this);

        // Alter Column operations
        expression.AlterColumnExpression?.Accept(this);

        // Create Column operations
        expression.AddColumnExpression?.Accept(this);
    }

    public void Visit(DvlSqlCreateColumnExpression expression)
    {
        _command.AppendLine(
            $"ALTER TABLE {_currentTable}{Environment.NewLine}" +
            $"ADD COLUMN {expression.Name} " +
            $"{Exts.ToSqlString(expression.Type.Value, expression.Size, expression.Precision, expression.Scale)} " +
            $"{(expression.IsNull ? "NULL" : "NOT NULL")};");

        // DEFAULT (must be separate)
        expression.DefaultExpression?.Accept(this);

        // PRIMARY KEY (separate constraint)
        expression.PrimaryKeyExpression?.Accept(this);

        // UNIQUE
        expression.UniqueExpression?.Accept(this);

        // FOREIGN KEY
        expression.ForeignKeyExpression?.Accept(this);

        // INDEX (separate statement)
        expression.IndexExpression?.Accept(this);
    }

    public void Visit(DvlSqlAlterColumnExpression expression)
    {
        // ALTER COLUMN (type / nullability)
        if (expression.Type != null)
        {
            _command.AppendLine(
                $"ALTER TABLE {_currentTable} ALTER COLUMN {expression.Name} " +
                $"{Exts.ToSqlString(expression.Type.Value, expression.Size, expression.Precision, expression.Scale)} " +
                $"{(expression.IsNull ? "NULL" : "NOT NULL")};");
        }

        // DEFAULT (must be separate)
        expression.DefaultExpression?.Accept(this);

        // PRIMARY KEY (separate constraint)
        expression.PrimaryKeyExpression?.Accept(this);

        // UNIQUE
        expression.UniqueExpression?.Accept(this);

        // FOREIGN KEY
        expression.ForeignKeyExpression?.Accept(this);

        // INDEX (separate statement)
        expression.IndexExpression?.Accept(this);
    }

    public void Visit(DvlSqlPrimaryKeyExpression expression)
    {
        _command.AppendLine(
            $"ALTER TABLE {_currentTable} " +
            $"ADD CONSTRAINT {expression.Name} PRIMARY KEY ({expression.ColumnName});");
    }

    public void Visit(DvlSqlDefaultExpression expression)
    {
        _command.AppendLine(
            $"ALTER TABLE {_currentTable} " +
            $"ADD CONSTRAINT {expression.Name} DEFAULT {expression.Value} FOR {expression.ColumnName};");
    }

    public void Visit(DvlSqlUniqueExpression expression)
    {
        _command.AppendLine(
            $"ALTER TABLE {_currentTable} " +
            $"ADD CONSTRAINT {expression.Name} UNIQUE ({expression.ColumnName});");
    }

    public void Visit(DvlSqlForeignKeyExpression expression)
    {
        _command.Append(
            $"ALTER TABLE {_currentTable} " +
            $"ADD CONSTRAINT {expression.Name} FOREIGN KEY ({expression.ColumnName}) " +
            $"REFERENCES {expression.ReferenceTableName}({expression.ReferenceColumnName});");
    }

    public void Visit(DvlSqlIndexExpression expression)
    {
        _command.AppendLine(
            $"CREATE {(expression.IsUnique ? "UNIQUE " : "")}INDEX {expression.Name} " +
            $"ON {_currentTable}({expression.ColumnName});");
    }

    public void Visit(DvlSqlDropConstraintExpression expression)
    {
        _command.AppendLine(
            $"ALTER TABLE {_currentTable} DROP CONSTRAINT {expression.Name};");
    }

    public void Visit(DvlSqlDropIndexExpression expression)
    {
        _command.AppendLine(
            $"DROP INDEX {expression.Name} ON {_currentTable};");
    }

    public void Visit(DvlSqlRenameColumnExpression expression)
    {
        _command.AppendLine(
            $"EXEC sp_rename '{_currentTable}.{expression.OldColumnName}', '{expression.NewColumnName}', 'COLUMN';");
    }

    public void Visit(DvlSqlDropColumnExpression expression)
    {
        _command.AppendLine(
            $"ALTER TABLE {_currentTable} DROP COLUMN {expression.Name};");
    }
}