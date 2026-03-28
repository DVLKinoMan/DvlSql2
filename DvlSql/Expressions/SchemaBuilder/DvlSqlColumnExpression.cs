using System.Data;

namespace DvlSql.Expressions;

public class DvlSqlColumnExpression(string name)
{
    public string Name { get; } = name;
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
    
    public void Accept(ISchemaBuilderVisitor visitor) => visitor.Visit(this);
}