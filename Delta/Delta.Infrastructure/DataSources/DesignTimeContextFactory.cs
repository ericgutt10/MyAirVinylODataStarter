using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Delta.Infrastructure.DataSources;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<WeatherDbCtx>
{
    static DesignTimeContextFactory()
    {
        _config ??= CreateConfiguration<DesignTimeContextFactory>();
    }

    private static readonly IConfiguration? _config;
    public static IConfiguration Config => _config!;

    public WeatherDbCtx CreateDbContext(string[] args)
    {
        var connStr = Config.GetConnectionString("DevelopConnection");
        var msg = $"ConnStr: {connStr}";
        Debug.WriteLine(msg);
        Console.WriteLine(msg);

        var optionsBuilder = new DbContextOptionsBuilder<WeatherDbCtx>();

        optionsBuilder.UseSqlServer(connStr);

        return new WeatherDbCtx(optionsBuilder.Options);
    }

    public static IConfiguration CreateConfiguration<T>(bool optional = false)
    where T : class
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"{HostAssy.HostName<T>()}.appsettings.json", optional: optional, reloadOnChange: true)
            .Build();
    }
}

public static class HostAssy
{
    public static string? HostName<T>()
        where T : class
    {
        return typeof(T).Assembly.GetName().Name;
    }
}