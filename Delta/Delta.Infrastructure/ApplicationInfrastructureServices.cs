using Blazr.OneWayStreet.Core;
using Blazr.OneWayStreet.Infrastructure;
using Delta.Core;
using Delta.Core.WeatherForecast;
using Delta.Infrastructure.DataSources;
using Delta.Infrastructure.DomObjects;
using Delta.Infrastructure.WeatherForecasts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
namespace Delta.Infrastructure;

public static class ApplicationInfrastructureServices
{
    /// <summary>
    /// Adds the server side Mapped Infrastructure services
    /// and generic handlers
    /// </summary>
    /// <param name="services"></param>
    public static void AddAppServerMappedInfrastructureServices(this IServiceCollection services)
    {
        //services.AddDbContextFactory<InMemoryTestDbContext>(options
        //    => options.UseInMemoryDatabase($"TestDatabase-{Guid.NewGuid().ToString()}"));

        services.AddScoped<IDataBroker, DataBroker>();

        // Add the standard handlers
        services.AddScoped<IListRequestHandler, ListRequestServerHandler<InMemoryTestDbContext>>();
        services.AddScoped<IItemRequestHandler, ItemRequestServerHandler<InMemoryTestDbContext>>();
        services.AddScoped<ICommandHandler, CommandServerHandler<InMemoryTestDbContext>>();
        
        // Add default KeyProviders
        services.AddScoped<IKeyProvider<Guid>, GuidKeyProvider>();
        services.AddScoped<IKeyProvider<long>, LongKeyProvider>();
        services.AddScoped<IKeyProvider<int>, IntKeyProvider>();

        // Add any individual entity services
        services.AddMappedWeatherForecastServerInfrastructureServices();
    }

    /// <summary>
    /// Adds the generic services for the API Data Pipeline infrastructure
    /// </summary>
    /// <param name="services"></param>
    /// <param name="baseHostEnvironmentAddress"></param>
    public static void AddAppClientMappedInfrastructureServices(this IServiceCollection services, string baseHostEnvironmentAddress)
    {
        services.AddHttpClient();
        services.AddHttpClient(AppDictionary.Common.WeatherHttpClient, client => { client.BaseAddress = new Uri(baseHostEnvironmentAddress); });

        services.AddScoped<IDataBroker, DataBroker>();

        services.AddScoped<IListRequestHandler, ListRequestAPIHandler>();
        services.AddScoped<IItemRequestHandler, ItemRequestAPIHandler>();
        services.AddScoped<ICommandHandler, CommandAPIHandler>();

        services.AddAppClientMappedWeatherForecastInfrastructureServices();
    }

    /// <summary>
    /// Adds specific WeatherForecast API call implementations
    /// </summary>
    /// <param name="services"></param>
    /// <param name="baseHostEnvironmentAddress"></param>
    public static void AddAppClientMappedWeatherForecastInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<INewRecordProvider<Core.WeatherForecast.DmoWeatherForecast>, NewWeatherForecastProvider>();

        //services.AddScoped<IListRequestHandler<DmoWeatherForecast>, WeatherForecastAPIListRequestHandler>();
        //services.AddScoped<IItemRequestHandler<DmoWeatherForecast, WeatherForecastId>, WeatherForecastAPIItemRequestHandler>();
        //services.AddScoped<ICommandHandler<DmoWeatherForecast>, WeatherForecastAPICommandHandler>();
    }

    /// <summary>
    ///  Adds the test data to the in-memory DB context
    /// </summary>
    /// <param name="provider"></param>
    //public static void AddTestData(IServiceProvider provider)
    //{
    //    var factory = provider.GetService<IDbContextFactory<InMemoryTestDbContext>>();

    //    if (factory is not null)
    //        TestDataProvider.Instance().LoadDbContext<InMemoryTestDbContext>(factory);
    //}

    /// <summary>
    /// Adds the Server side Mapped Handlers for WeatherForecasts
    /// </summary>
    /// <param name="services"></param>
    public static void AddMappedWeatherForecastServerInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IKeyProvider<WeatherForecastId>, WeatherForecastKeyProvider>();

        services.AddScoped<IDboEntityMap<DmoWeatherForecast, DmoWeatherForecast>, DmoWeatherForecastMap>();
        services.AddScoped<IListRequestHandler<DmoWeatherForecast>, MappedListRequestServerHandler<InMemoryTestDbContext, DmoWeatherForecast, DmoWeatherForecast>>();
        services.AddScoped<IItemRequestHandler<DmoWeatherForecast, WeatherForecastId>, MappedItemRequestServerHandler<InMemoryTestDbContext, DmoWeatherForecast, DmoWeatherForecast, WeatherForecastId>>();
        services.AddScoped<ICommandHandler<DmoWeatherForecast>, MappedCommandServerHandler<InMemoryTestDbContext, DmoWeatherForecast, DmoWeatherForecast>>();

        services.AddTransient<IRecordFilterHandler<DomObjects.DmoWeatherForecast>, WeatherForecastFilterHandler>();
        services.AddTransient<IRecordSortHandler<DomObjects.DmoWeatherForecast>, WeatherForecastSortHandler>();

        services.AddScoped<INewRecordProvider<Core.WeatherForecast.DmoWeatherForecast>, NewWeatherForecastProvider>();
    }
}