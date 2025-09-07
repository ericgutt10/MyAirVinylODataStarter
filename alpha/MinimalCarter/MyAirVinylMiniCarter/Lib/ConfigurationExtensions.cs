using Serilog;
using Serilog.Core;
using Serilog.Settings.Configuration;

namespace MyAirVinylMiniCarter.Lib;

public static class CreateLoggerFactory
{
    private static Logger? _logger;

    private static readonly ConfigurationReaderOptions options = new(typeof(ConsoleLoggerConfigurationExtensions).Assembly, typeof(Logger).Assembly);

    public static Serilog.ILogger Logger<T>(this
        IHostApplicationBuilder _,
        IConfiguration configuration) where T : class =>
            _logger ??= new LoggerConfiguration()
                .ReadFrom.Configuration(configuration, options)
                .WriteTo.Console()
                .WriteTo.File(HostAssy.LogPath<T>(), shared: true)
                .Enrich.FromLogContext()
                .CreateLogger();

    public static LoggerConfiguration Logger<T>(this
        LoggerConfiguration lc,
        IConfiguration configuration) where T : class => lc
                .ReadFrom.Configuration(configuration)//, options)
                .WriteTo.Console()
                .WriteTo.File(HostAssy.LogPath<T>(), shared: true)
                .Enrich.FromLogContext();
}

public static class CreateConfigurationFactory
{
    private static IConfiguration? _config;

    public static IConfiguration CreateConfiguration(this
        IServiceCollection _,
        bool reloadOnChange = true
        )
    {
        return _config ??= new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange)
            .Build();
    }
}

public static class ConfigurationExtensions
{
    public static (string? connection, int retry, int timeout) GetDbConfig(this
        IServiceCollection builder,
        string connectionName)
    {
        var config = builder.CreateConfiguration();
        var connection = config[connectionName];

        string? conn = config?.GetConnectionString(connection ?? "");
        int timeout = int.TryParse(config?["AppSettings:SqlCmdTimeoutSeconds"]
            , out timeout) ?
            timeout : 120;
        int retries = 3;

        return (conn, retries, timeout);
    }
}