using Microsoft.FluentUI.AspNetCore.Components;

namespace Beta.UI.Features.Weather._Lib;

public class WeatherProvider(WeatherApiClient client)
{
    private const int MAX_COUNT = 500;
    private List<WeatherForecast>? _weather;

    public WeatherApiClient Client { get; } = client;

    public async ValueTask<GridItemsProviderResult<WeatherForecast>> GetItemsAsync(GridItemsProviderRequest<WeatherForecast> request, CancellationToken cancellationToken)
    {
        _weather ??= (await GetDataAsync(MAX_COUNT, cancellationToken))?.ToList();

        var query = _weather?
            .Skip(request.StartIndex)
            .Take(request.Count ?? 10);

        return new GridItemsProviderResult<WeatherForecast>() { Items = query?.ToList() ?? [], TotalItemCount = _weather?.Count ?? 0 };
    }

    protected async Task<ICollection<WeatherForecast>?> GetDataAsync(int maxCount, CancellationToken cancellationToken)
    {
        return await Client.GetWeatherAsync(maxCount, cancellationToken);
    }
}