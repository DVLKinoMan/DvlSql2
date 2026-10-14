using System.Data;
using NUnit.Framework;

namespace DvlSql.SqlServer.Tests.SchemaProvider;

[TestFixture]
public class GetAllTables
{
    private readonly DvlSqlMs _sql =
        new(
            new(){ ConnectionString = @"Server=LAPTOP-DEUOP46M\LOCALHOST;Database=TestDatabase;TrustServerCertificate=True;MultipleActiveResultSets=true;Integrated Security=True;"
            });


    [Test]
    public async Task TestMethod3()
    {
        var actual = await _sql
            .GetAllTablesAsync();
    }
}