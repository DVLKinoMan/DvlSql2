using DvlSql;
using DvlSql.PostgreSql;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    // ReSharper disable once InconsistentNaming
    public static IServiceCollection AddDvlPostgreSql(this IServiceCollection services, DvlSqlOptions options)
    {
        services.AddSingleton<IDvlSql>(_ => new DvlPostgreSql(options.ConnectionString));
        return services;
    }

    // ReSharper disable once InconsistentNaming
    public static IServiceCollection AddDvlPostgreSql(this IServiceCollection services, Action<DvlSqlOptions> options)
    {
        services.Configure(options);

        services.AddSingleton<IDvlSql>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<DvlSqlOptions>>().Value;

            return new DvlPostgreSql(options.ConnectionString);
        });
        return services;
    }
}