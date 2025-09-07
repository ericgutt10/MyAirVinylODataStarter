/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
///
using System.ComponentModel.DataAnnotations;

namespace Delta.Core.WeatherForecast;

public sealed record WeatherForecast : ICommandEntity
{
    [Key] public Guid WeatherForecastUid { get; init; }

    public DateOnly Date { get; init; }

    public int TemperatureC { get; init; }

    public string? Summary { get; init; }
}

public readonly record struct WeatherForecastId(Guid Value)
{
    public static WeatherForecastId NewEntity => new(Guid.Empty);
};

[APIInfo(pathName: "WeatherForecast", clientName: AppDictionary.Common.WeatherHttpClient)]
public sealed record DmoWeatherForecast : ICommandEntity
{
    public WeatherForecastId WeatherForecastId { get; init; } = new(Guid.Empty);
    public DateOnly Date { get; init; }
    public Temperature Temperature { get; set; } = new(0);
    public string? Summary { get; set; }
}