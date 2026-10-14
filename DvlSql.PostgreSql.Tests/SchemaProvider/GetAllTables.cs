using NUnit.Framework;

namespace DvlSql.PostgreSql.Tests.SchemaProvider;

[TestFixture]
public class GetAllTables
{
    private readonly DvlPostgreSql _sql =
        new(
            new(){ ConnectionString = StaticConnectionStrings.ConnectionStringForTest  });


    [Test]
    public async Task TestMethod3()
    {
        var actual = await _sql
            .GetAllTablesAsync();
    }
}