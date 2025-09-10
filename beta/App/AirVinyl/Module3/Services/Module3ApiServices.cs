using AirVinyContext.Entities;
using App.AirVinyl.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.AirVinyl.Module3.Services;

public static class Module3ApiServices
{
    public static IServiceCollection AddModule3ApiServices<T>(this IServiceCollection services)
        where T : class
    {
        // Register any services specific to the App.Suppliers module here
        // For example, you might register a mediator or a database context

        services.AddDbContextFactory<MyAirVinylCtx>(opt =>
        {
            var (connection, retry, timeout) = services.GetDbConfig<T>("AppSettings:ConnectionName");
            opt.UseSqlServer(connection,
            opt =>
            {
                opt.EnableRetryOnFailure(retry);
                opt.CommandTimeout(timeout);
            });
            opt.EnableSensitiveDataLogging();
            opt.EnableDetailedErrors();
        });

        // Add any individual entity services
        return services
            ;
    }
}