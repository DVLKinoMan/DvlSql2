using System.Data;
using NUnit.Framework;

namespace DvlSql.SqlServer.Tests.Tables;

[TestFixture]
public class Alter
{
    private readonly DvlSqlMs _sql1 =
        new(
            new()
            {
                ConnectionString =
                    "Server=LAPTOP-DEUOP46M\\LOCALHOST;Database=TestDatabase;TrustServerCertificate=True;MultipleActiveResultSets=true;Integrated Security=True;"
            });

    [Test]
    public async Task AlertWithNotNullableNewColumn()
    {
        await _sql1
            .AlterTable("TestTable")
            .WithColumn(Guid.NewGuid().ToString()).AsType(SqlDbType.Int).AsNotNull()
            .AsDefault(Guid.NewGuid().ToString(), "0")
            .ExecuteAsync();
    }

    [Test]
    public async Task AlertWithNullableNewColumn()
    {
        Random r = new Random();
        int k = r.Next(100_000);
        await _sql1
            .AlterTable("TestTable")
            .WithColumn("col" + k).AsType(SqlDbType.VarChar, 10).AsNull()
            .HasIndex("index" + k)
            .ExecuteAsync();
    }
}