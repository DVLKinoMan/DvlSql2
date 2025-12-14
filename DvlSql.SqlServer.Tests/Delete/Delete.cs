using System.Text.RegularExpressions;
using NUnit.Framework;
using static DvlSql.ExpressionHelpers;

namespace DvlSql.SqlServer.Tests.Delete;

[TestFixture]
public class Delete
{
    private readonly DvlSqlMs _sql =
        new(
            new(){ ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1"});

    [Test]
    public void TestMethod1()
    {
        var actualDelete = _sql.DeleteFrom("dbo.Words")
            .Where(ConstantExpCol("Text") == "New Text")
            .ToString();

        string expectedDelete = Regex.Escape(
            $"DELETE FROM dbo.Words{Environment.NewLine}" +
            $"WHERE Text = @p1");

        Assert.That(Regex.Escape(actualDelete!), Is.EqualTo(expectedDelete));
    }

    [Test]
    public void TestMethod2()
    {
        var actualDelete = _sql.DeleteFrom("dbo.Words")
            .ToString();

        string expectedDelete = Regex.Escape(
            @"DELETE FROM dbo.Words");

        Assert.That(Regex.Escape(actualDelete!), Is.EqualTo(expectedDelete));
    }

    [Test]
    public void TestMethod3()
    {
        var actualDelete = _sql.DeleteFrom("dbo.Words")
            .Output(r => r["deleted.id"], "deleted.Id")
            .Where(ConstantExpCol("Id") == 2)
            .ToString();

        string expectedDelete = Regex.Escape(
            $@"DELETE FROM dbo.Words{Environment.NewLine}OUTPUT deleted.Id{Environment.NewLine}WHERE Id = @p1");

        Assert.That(Regex.Escape(actualDelete!), Is.EqualTo(expectedDelete));
    }

    [Test]
    public void TestMethod4()
    {
        var actualDelete = _sql.DeleteFrom(FromExp("Words", "w"))
            .Join<int>("Words", "w.Id", "Words.Id")
            .Where(ConstantExpCol("Id") == 2)
            .ToString();

        string expectedDelete = Regex.Escape(
            $@"DELETE w FROM Words AS w{Environment.NewLine}INNER JOIN Words ON w.Id = Words.Id{Environment.NewLine}WHERE Id = @p1");

        Assert.That(Regex.Escape(actualDelete!), Is.EqualTo(expectedDelete));
    }
}