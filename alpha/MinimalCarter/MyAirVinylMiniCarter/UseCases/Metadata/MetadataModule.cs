using Carter;
using Microsoft.AspNetCore.OData;
using MyAirVinylMiniCarter.UseCases.AirVinyl.EdmModel;

namespace MyAirVinylMiniCarter.UseCases.Metadata;

public class MetadataModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapODataServiceDocument("v1/$document", EdmModelBuilder.AirVinylModel);

        app.MapODataServiceDocument("v2/$document", EdmModelBuilder.AirVinylModel)
            .WithODataBaseAddressFactory(c => new Uri("http://localhost:5177/v2"));

        app.MapODataMetadata("v1/$metadata", EdmModelBuilder.AirVinylModel);
    }
}