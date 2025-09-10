using System.Reflection;

namespace App.AirVinyl.Lib;

public static class HostAssy
{
    private static string? _typName;
    private static string? _logPath;
    private static Assembly? _assy;
    private static AssemblyConfigurationAttribute? _assyAttr;
#pragma warning disable CS0649 // Field 'HostAssy._configName' is never assigned to, and will always have its default value null
    private static readonly string? _configName;
#pragma warning restore CS0649 // Field 'HostAssy._configName' is never assigned to, and will always have its default value null

    public static Assembly Host<T>() where T : class => _assy ??= typeof(T).Assembly;

    public static AssemblyConfigurationAttribute? HostConfigAttr<T>() where T : class =>
        _assyAttr ??= typeof(T)?.Assembly?.GetCustomAttribute<AssemblyConfigurationAttribute>();

    public static string? HostConfigName<T>() where T : class => _configName ??
        HostConfigAttr<T>()?.Configuration;

    public static string HostName<T>() where T : class
    {
        _typName ??= Host<T>().GetName().Name;
        _ = _typName is null ? throw new NullReferenceException() : true;

        return _typName;
    }

    public static string LogPath<T>() where T : class => _logPath ??=
        Path.Combine(
            Path.GetTempPath(),
            HostName<T>(),
            $@"{HostName<T>()}-{DateTime.Now:yyyy-MM-dd-HHmmssffff}.log"
            );
}