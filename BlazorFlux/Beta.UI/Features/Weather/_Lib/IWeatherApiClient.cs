
namespace Beta.UI.Features.Weather._Lib
{
    public interface IWeatherApiClient
    {
        Task<ICollection<WeatherForecast>?> GetWeatherAsync(int maxItems = 10, CancellationToken cancellationToken = default);
    }
}