/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

using Blazr.OneWayStreet.Core;
using Delta.Infrastructure.DomObjects;
using System.Linq.Expressions;

namespace Delta.Infrastructure.WeatherForecasts;

public class WeatherForecastFilterBySummarySpecification : PredicateSpecification<DmoWeatherForecast>
{
    private string? _summary;

    public WeatherForecastFilterBySummarySpecification()
    { }

    public WeatherForecastFilterBySummarySpecification(FilterDefinition filter)
    {
        _summary = filter.FilterData;
    }

    public override Expression<Func<DmoWeatherForecast, bool>> Expression
        => item => item.Summary != null ? item.Summary.Equals(_summary) : false;
}
