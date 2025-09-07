using UuidExtensions;

namespace Delta.Core.WeatherForecast;

public class WeatherForecastKeyProvider : IKeyProvider<WeatherForecastId>
{
    public WeatherForecastId GetKey(object key)
    {
        if (key is Guid value)
            return new WeatherForecastId(value);

        throw new InvalidKeyProviderException();
    }

    public object GetValueObject(WeatherForecastId key)
        => key.Value;

    public WeatherForecastId GetNew()
        => new(Uuid7.Guid());

    public bool IsDefault(WeatherForecastId key)
        => key.Value == Guid.Empty;
}