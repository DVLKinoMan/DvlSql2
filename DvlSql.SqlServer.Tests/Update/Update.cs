using System.Text.RegularExpressions;
using NUnit.Framework;
using static DvlSql.ExpressionHelpers;
using static DvlSql.Extensions.SqlType;

namespace DvlSql.SqlServer.Tests.Update;

[TestFixture]
public class Update
{
    private readonly DvlSqlMs _sql =
        new(
            new(){ ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1"});

    [Test]
    public void TestMethod1()
    {
        var actualUpdate = _sql.Update("dbo.Words")
            .Set(Money("money", new(2.11)))
            .ToString();

        string expectedUpdate = Regex.Escape(
            $"UPDATE dbo.Words{Environment.NewLine}SET money = @money");

        Assert.That(Regex.Escape(actualUpdate!), Is.EqualTo(expectedUpdate));
    }

    [Test]
    public void TestMethod2()
    {
        var actualUpdate = _sql.Update("dbo.Words")
            .Set(Money("money", new(3.11)))
            .Where(ConstantExpCol("Amount") == new decimal(42))
            .ToString();

        string expectedUpdate = Regex.Escape(
            string.Format("UPDATE dbo.Words{0}" +
                          "SET money = @money{0}" +
                          "WHERE Amount = @p1",
                Environment.NewLine));

        Assert.That(Regex.Escape(actualUpdate!), Is.EqualTo(expectedUpdate));
    }

    [Test]
    public void TestMethod3()
    {
        var actualUpdate = _sql.Update("dbo.Words")
            .Set(Money("money", new(3.11)))
            .Set(Bit("isSome", true))
            .Set(Float("floatNumber", 1.222))
            .Set(BigInt("bigint", 1111111111))
            .Set(Xml("xml", "<xml></xml>"))
            .Set(DateTime("Date", System.DateTime.Now))
            .Where(ConstantExpCol("Amount") == new decimal(42))
            .ToString();

        string expectedUpdate = Regex.Escape(
            string.Format("UPDATE dbo.Words{0}" +
                          "SET money = @money, isSome = @isSome, floatNumber = @floatNumber, bigint = @bigint, xml = @xml, Date = @Date{0}" +
                          "WHERE Amount = @p1",
                Environment.NewLine));

        Assert.That(Regex.Escape(actualUpdate!), Is.EqualTo(expectedUpdate));
    }

    [Test]
    public void TestMethod4()
    {
        string folderPath = "some/path";
        string exactValue = $"\'{folderPath}\' + {ConvertExp("nvarchar(260)", "Id")}";

        var someIds = new Guid[] { new(), new(), new() };
        var actualUpdate = _sql.Update("dbo.Words")
            .Set(NVarCharWithExactValue("RelativePath", exactValue, 260))
            .Where(InExp("Id", someIds.Select(id => ConstantExp(id)).ToArray()))
            .ToString();

        string expectedUpdate = Regex.Escape(
            string.Format("UPDATE dbo.Words{0}" +
                          "SET RelativePath = " + exactValue + "{0}" +
                          "WHERE Id IN ( @p1, @p2, @p3 )",
                Environment.NewLine));

        Assert.That(Regex.Escape(actualUpdate!), Is.EqualTo(expectedUpdate));
    }
}