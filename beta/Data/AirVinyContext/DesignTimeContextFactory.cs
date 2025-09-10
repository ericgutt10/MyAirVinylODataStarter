using AirVinyContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace AirVinyContext;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<MyAirVinylCtx>
{

    static DesignTimeContextFactory()
    {
        _config ??= CreateConfiguration<DesignTimeContextFactory>();
    }

    private static readonly IConfiguration? _config;
    public static IConfiguration Config => _config!;

    public MyAirVinylCtx CreateDbContext(string[] args)
    {
        var connStr = Config.GetConnectionString("DevelopConnection");
        var msg = $"ConnStr: {connStr}";
        Debug.WriteLine(msg);
        Console.WriteLine(msg);

        var optionsBuilder = new DbContextOptionsBuilder<MyAirVinylCtx>();

        optionsBuilder.UseSqlServer(connStr);

        return new MyAirVinylCtx(optionsBuilder.Options);
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