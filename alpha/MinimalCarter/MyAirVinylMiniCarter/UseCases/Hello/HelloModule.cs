using AirVinyContext.Entities;
using Carter;

namespace MyAirVinylMiniCarter.UseCases.Hello;

public class HelloModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Group
        var group = app.MapGroup("")
            //    .WithOData2(metadata => metadata.IsODataFormat = true, services => services.AddSingleton<IFilterBinder>(new FilterBinder()))
            ;

        group.MapGet("v1", () => "hello v1")
            .AddEndpointFilter(async (efiContext, next) =>
            {
                var logger = app.ServiceProvider.GetService<ILogger<Program>>();
                var endpoint = efiContext.HttpContext.GetEndpoint();
                logger?.LogInformation("----Before calling");
                var result = await next(efiContext);
                logger?.LogInformation($"----After calling, {result?.GetType().Name}");
                return result;
            }
            ).Finally(v =>
            {
                v.Metadata.Add(new Person());
            });
        group.MapGet("v2", () => "hello v2");
    }
}
