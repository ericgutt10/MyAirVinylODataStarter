using Delta.Core.Common;

/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
namespace Delta.Core.WeatherForecast;

public sealed class WeatherForecastEditContext : IRecordEditContext<DmoWeatherForecast>
{
    public DmoWeatherForecast BaseRecord { get; private set; }
    public bool IsDirty => BaseRecord != AsRecord;

    [TrackState] public string? Summary { get; set; }
    [TrackState] public decimal Temperature { get; set; }
    [TrackState] public DateTime? Date { get; set; }

    public DmoWeatherForecast AsRecord =>
        BaseRecord with
        {
            Date = DateOnly.FromDateTime(Date ?? DateTime.Now),
            Summary = Summary,
            Temperature = new(Temperature)
        };

    public WeatherForecastEditContext()
    {
        BaseRecord = new DmoWeatherForecast();
        Load(BaseRecord);
    }

    public WeatherForecastEditContext(DmoWeatherForecast record)
    {
        BaseRecord = record;
        Load(record);
    }

    public IDataResult Load(DmoWeatherForecast record)
    {
        var alreadyLoaded = BaseRecord.WeatherForecastId != WeatherForecastId.NewEntity;

        if (alreadyLoaded)
            return DataResult.Failure("A record has already been loaded.  You can't overload it.");

        BaseRecord = record;
        Summary = record.Summary;
        Temperature = record.Temperature.TemperatureC;
        Date = record.Date.ToDateTime(TimeOnly.MinValue);
        return DataResult.Success();
    }
}
