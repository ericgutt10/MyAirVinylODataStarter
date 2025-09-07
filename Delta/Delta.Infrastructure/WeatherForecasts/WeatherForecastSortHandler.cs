using Blazr.OneWayStreet.Core;
using Delta.Infrastructure.DomObjects;

/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
namespace Delta.Infrastructure.WeatherForecasts;

public class WeatherForecastSortHandler : RecordSortHandler<DmoWeatherForecast>, IRecordSortHandler<DmoWeatherForecast>
{
    public WeatherForecastSortHandler()
    {
        DefaultSorter = (item) => item.Date;
        DefaultSortDescending = true;
        PropertyNameMap = new Dictionary<string, string>()
            {
                {"Temperature.TemperatureC", "Temperature" },
                {"Temperature.TemperatureF", "Temperature" }
            };
    }
}
