
namespace DvlSql.PostgreSql.Tests
{
    public static class StaticConnectionStrings
    {
        public static readonly string ConnectionStringForTest =
            @"Server=localhost;Port=5432;Database=DvlListsDb;Username=postgres;Password=$passW0rd";
    }
}