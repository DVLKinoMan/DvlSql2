using NUnit.Framework;
using static DvlSql.Extensions.SqlType;
using static DvlSql.Extensions.DataReader;
using static DvlSql.ExpressionHelpers;

namespace DvlSql.SqlServer.Tests.Transaction;

[TestFixture]
public class Transactions
{
    private readonly DvlSqlMs _sql =
        new(
            new(){ ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1"});

    //todo normal test
    //[Test]
    public async Task TestMethod1()
    {
        var conn = await _sql.BeginTransactionAsync();
        await _sql.SetConnection(conn).DeleteFrom("dbo.Words")
            .ExecuteAsync();
        _ = await _sql.SetConnection(conn).InsertInto<(int, string)>("dbo.Words",
                IntType("Id"), NVarCharType("Name", 50))
            .Values((1, "Some New Word"), (2, "Some New Word 2"))
            .ExecuteAsync();
        _ = await _sql.SetConnection(conn).Update("dbo.Words")
            .Set(NVarChar("Name", "Updated Word", 50))
            .Where(ConstantExpCol("Id") == 2)
            .ExecuteAsync();

        await _sql.SetConnection(conn).CommitAsync();
    }


    //todo normal test
    //[Test]
    public async Task TestMethod2()
    {
        var table = _sql.DeclareTable("inserted")
            .AddColumns(IntType("id", true));

        var conn = await _sql.BeginTransactionAsync();
        await _sql.SetConnection(conn).DeleteFrom("dbo.Words")
            .ExecuteAsync();

        var k = await _sql.SetConnection(conn).InsertInto<(int, string)>("dbo.Words",
                IntType("Id"), NVarCharType("Name", 50))
            .Output(AsList (r=>int.Parse(r["id"].ToString()!)),"inserted.id")
            .Values((1, "Some New Word"), (2, "Some New Word 2"))
            .ExecuteAsync();

        await _sql.SetConnection(conn).CommitAsync();
    }
}