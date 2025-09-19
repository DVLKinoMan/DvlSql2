// See https://aka.ms/new-console-template for more information

using DvlSql.PostgreSql;

DvlPostgreSql _sql1 =
    new(
        "Server=localhost;User ID=postgres;Password=1234;Database=Space.Service.Transfer.Api;Application Name=Space.Service.Transfer.Api");

var result = await _sql1.From("\"TransfersArchive\"")
    .Select("\"Id\"", "\"Status\"")
    .ToListAsync(r => (Id: r["Id"].ToString(), Status: r["Status"].ToString()));

result.ForEach(r =>
{
    Console.WriteLine("Id: " + r.Id);
    Console.WriteLine("Status: " + r.Status);
}); 