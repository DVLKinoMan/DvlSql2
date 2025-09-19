using DvlSql;
using DvlSql.SqlServer;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    // ReSharper disable once InconsistentNaming
    public static IServiceCollection AddDvlSqlMS(this IServiceCollection services, DvlSqlOptions options)
    {
        services.AddSingleton<IDvlSql>(provider => new DvlSqlMs(options.ConnectionString));
        return services;
    }

    // ReSharper disable once InconsistentNaming
    public static IServiceCollection AddDvlSqlMS(this IServiceCollection services, Action<DvlSqlOptions> options)
    {
        services.Configure(options);

        services.AddSingleton<IDvlSql>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<DvlSqlOptions>>().Value;

            return new DvlSqlMs(options.ConnectionString);
        });
        return services;
    }
}
