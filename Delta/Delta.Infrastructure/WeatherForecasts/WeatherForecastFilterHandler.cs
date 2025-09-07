using Blazr.OneWayStreet.Core;
using Delta.Core;
using Delta.Infrastructure.DomObjects;

/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
namespace Delta.Infrastructure.WeatherForecasts;

public class WeatherForecastFilterHandler : RecordFilterHandler<DmoWeatherForecast>, IRecordFilterHandler<DmoWeatherForecast>
{
    public override IPredicateSpecification<DmoWeatherForecast>? GetSpecification(FilterDefinition filter)
        => filter.FilterName switch
        {
            AppDictionary.WeatherForecast.WeatherForecastFilterBySummarySpecification => new WeatherForecastFilterBySummarySpecification(filter),
            _ => null
        };
}
