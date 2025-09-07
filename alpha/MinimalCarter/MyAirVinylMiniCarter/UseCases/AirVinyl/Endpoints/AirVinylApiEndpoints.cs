using AirVinyContext.Entities;
using Carter;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using MyAirVinylMiniCarter.UseCases.AirVinyl.EdmModel;
using static MyAirVinylMiniCarter.UseCases.AirVinyl.Handlers.AirVinylHandlers;

namespace MyAirVinylMiniCarter.UseCases.AirVinyl.Endpoints;

public class AirVinylApiEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/People", (MyAirVinylCtx ctx) =>
        {
            ctx.People.Include(p => p.VinylRecords);
        });

        app.MapGet("/odata/People", (MyAirVinylCtx ctx) =>
        {
            ctx.People.Include(p => p.VinylRecords);
        })
            .WithODataResult();
        // .WithODataModel(EdmModelBuilder.AirVinylModel)
        // .WithOpenApi();
    }
}