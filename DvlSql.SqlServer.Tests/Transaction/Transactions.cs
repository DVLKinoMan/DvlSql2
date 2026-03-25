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
        var tran = await _sql.BeginTransactionAsync(async transaction =>
        {
            await transaction.DeleteFrom("dbo.Words")
                .ExecuteAsync();

            await transaction.InsertInto<(int, string)>("dbo.Words",
                    IntType("Id"), NVarCharType("Name", 50))
                .Values((1, "Some New Word"), (2, "Some New Word 2"))
                .ExecuteAsync();
            
            await transaction.Update("dbo.Words")
                .Set(NVarChar("Name", "Updated Word", 50))
                .Where(ConstantExpCol("Id") == 2)
                .ExecuteAsync();
        });

        await tran.CommitAsync();
    }


    //todo normal test
    //[Test]
    public async Task TestMethod2()
    {
        var table = _sql.DeclareTable("inserted")
            .AddColumns(IntType("id", true));

        await _sql.ExecuteTransactionAsync(async transaction =>
        {
            await transaction.DeleteFrom("dbo.Words")
                .ExecuteAsync();

            await transaction.InsertInto<(int, string)>("dbo.Words",
                    IntType("Id"), NVarCharType("Name", 50))
                .Output(AsList (r=>int.Parse(r["id"].ToString()!)),"inserted.id")
                .Values((1, "Some New Word"), (2, "Some New Word 2"))
                .ExecuteAsync();
            
            await transaction.Update("dbo.Words")
                .Set(NVarChar("Name", "Updated Word", 50))
                .Where(ConstantExpCol("Id") == 2)
                .ExecuteAsync();
        });
    }
}