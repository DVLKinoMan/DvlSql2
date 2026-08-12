using System.Data;

namespace DvlSql.Expressions;

public abstract class DvlSqlColumnExpression(string name, string? associatedName = null)
{
    public string Name { get; } = name;
    public string? AssociatedName { get; set; } = associatedName;
    public SqlDbType? Type { get; set; }
    public int? Size { get; set; }
    public byte? Precision { get; set; }
    public byte? Scale { get; set; }
    public bool IsNull { get; set; } = true;
    public DvlSqlPrimaryKeyExpression? PrimaryKeyExpression { get; set; }
    public DvlSqlDefaultExpression? DefaultExpression { get; set; }
    public DvlSqlForeignKeyExpression? ForeignKeyExpression { get; set; }
    public DvlSqlIndexExpression? IndexExpression { get; set; }
    public DvlSqlUniqueExpression? UniqueExpression { get; set; }
}