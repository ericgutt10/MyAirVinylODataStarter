using System.Net.Http.Json;

namespace Beta.UI.Features.Weather._Lib;

public class WeatherApiClient(HttpClient httpClient) : IWeatherApiClient
{
    private ICollection<WeatherForecast>? forecasts;

    public HttpClient HttpClient { get; } = httpClient;

    public async Task<ICollection<WeatherForecast>?> GetWeatherAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        return forecasts ??= await HttpClient.GetWeatherDataAsync(maxItems, cancellationToken);
    }
}

public static class WeatherApiClientExtensions
{
    public static async Task<ICollection<WeatherForecast>?> GetWeatherDataAsync(this HttpClient httpClient, int maxItems, CancellationToken cancellationToken)
    {
        List<WeatherForecast>? forecasts=[];
        await foreach (var forecast in httpClient.GetFromJsonAsAsyncEnumerable<WeatherForecast>("/weatherforecast", cancellationToken))
        {
            if (forecasts?.Count >= maxItems)
            {
                break;
            }
            if (forecast is not null)
            {
                forecasts?.Add(forecast);
            }
        }
        return forecasts;
    }
}
