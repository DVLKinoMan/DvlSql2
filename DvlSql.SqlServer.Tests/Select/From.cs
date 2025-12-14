using System.Text.RegularExpressions;
using NUnit.Framework;
using static DvlSql.ExpressionHelpers;

namespace DvlSql.SqlServer.Tests.Select;

[TestFixture]
public class From
{
    private readonly DvlSqlMs _sql =
        new(
            new(){ ConnectionString = 
            StaticConnectionStrings.ConnectionStringForTest});

    [Test]
    [TestCase("dbo.Words")]
    public void WithoutSelect(string tableName)
    {
        var from = _sql.From(tableName).ToString();

        var expectedSelect = Regex.Escape($"SELECT * FROM {tableName}");
        Assert.That(Regex.Escape(from!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("dbo.Words")]
    public void WithoutNoLock(string tableName)
    {
        var actualSelect1 = _sql.From(tableName)
            .Select()
            .ToString();
            
        var actualSelect2 = _sql.From(tableName, false)
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape($"SELECT * FROM {tableName}");
        Assert.Multiple(() =>
        {
            Assert.That(Regex.Escape(actualSelect1!), Is.EqualTo(expectedSelect));
            Assert.That(Regex.Escape(actualSelect2!), Is.EqualTo(expectedSelect));
        });
    }

    [Test]
    [TestCase("dbo.Words")]
    public void WithNoLock(string tableName)
    {
        var actualSelect = _sql.From(tableName, true)
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape($"SELECT * FROM {tableName} WITH(NOLOCK)");
        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("dbo.Words")]
    public void WithInnerSelect(string tableName)
    {
        ArgumentNullException.ThrowIfNull(tableName);

        var asName = "W";
        var fullSelect = FullSelectExp(SelectExp(), FromExp("dbo.Words"), @as: AsExp(asName));
        var actualSelect = _sql.From(fullSelect)
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape($"SELECT * FROM (SELECT * FROM dbo.Words) AS {asName}");
        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }
}