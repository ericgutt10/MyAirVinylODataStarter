using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.AirVinyl.Lib;

public static class CreateConfigurationFactory
{
    private static IConfiguration? _config;

    //public static IConfiguration CreateConfiguration(this
    //    IServiceCollection _,
    //    bool reloadOnChange = true
    //    )
    //{
    //    return _config ??= new ConfigurationBuilder()
    //        .AddJsonFile($"appsettings.json", optional: false, reloadOnChange)
    //        .Build();
    //}

    public static IConfiguration CreateConfiguration<T>(this
    IServiceCollection _,
    bool reloadOnChange = true
    ) where T : class
    {
        return _config ??= new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange)
            .AddJsonFile($"appsettings.{HostAssy.HostConfigName<T>()}.json", optional: true, reloadOnChange)
            .Build();
    }
}

public static class ConfigurationExtensions
{
    public static (string? connection, int retry, int timeout) GetDbConfig<T>(this
        IServiceCollection services,
        string connectionName) where T : class
    {
        var config = services.CreateConfiguration<T>();
        var connection = config[connectionName];

        string? conn = config?.GetConnectionString(connection ?? "");
        int timeout = int.TryParse(config?["AppSettings:SqlCmdTimeoutSeconds"]
            , out timeout) ?
            timeout : 120;
        int retries = 3;

        return (conn, retries, timeout);
    }
}