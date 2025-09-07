using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using MyAirVinylCarter;

public class AirVinylModule : ICarterModule
{
    readonly IEdmModel? model;
    public AirVinylModule()
    {
        model = EdmModelBuilder.GetEdmModel();
    }
     

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapODataServiceDocument("v1/$document", model);

        app.MapODataServiceDocument("v2/$document", model)
            .WithODataBaseAddressFactory(c => new Uri("http://localhost:5177/v2"));

        app.MapODataMetadata("v1/$metadata", model);

    }
}
