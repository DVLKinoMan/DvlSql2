using System.Data;
using NUnit.Framework;

namespace DvlSql.SqlServer.Tests.Tables;

[TestFixture]
public class Create
{  
    private readonly DvlSqlMs _sql1 =
        new(
            new(){ ConnectionString = 
                "Server=LAPTOP-DEUOP46M\\LOCALHOST;Database=TestDatabase;TrustServerCertificate=True;MultipleActiveResultSets=true;Integrated Security=True;"});

    [Test]
    public async Task CreateTableTest()
    {
        await _sql1
            .CreateTable(Guid.NewGuid().ToString())
            .WithColumn("Column1Id").AsPrimaryKey("key1")
                .AsType(SqlDbType.UniqueIdentifier).AsNotNull()
            .WithColumn("Column2Int").AsType(SqlDbType.Int).AsDefault("defaultInt", "0")
            .ExecuteAsync();
    }
}