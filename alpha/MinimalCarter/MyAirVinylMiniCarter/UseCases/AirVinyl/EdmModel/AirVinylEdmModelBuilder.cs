using AirVinyContext.Entities;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace MyAirVinylMiniCarter.UseCases.AirVinyl.EdmModel;

public static class EdmModelBuilder
{
    private static IEdmModel? _model;

    private static IEdmModel GetAirVinylEntityDataModel()
    {
        var builder = new ODataConventionModelBuilder
        {
            Namespace = "AirVinyl",
            ContainerName = "AirVinylContainier"
        };
        // Define entity sets
        builder.EntitySet<Person>("People");
        builder.EntitySet<VinylRecord>("VinylRecords");

        //builder.EntitySet<AirVinyContext.Entities.RecordStore>("RecordStores");
        //builder.EntitySet<AirVinyContext.Entities.Rating>("Ratings");
        //builder.EntitySet<AirVinyContext.Entities.PressingDetail>("PressingDetails");
        //builder.EntitySet<AirVinyContext.Entities.lib.SpecializedRecordStore>("SpecializedRecordStores");

        // Define complex types if any (none in this case)
        // Define actions and functions if any (none in this case)

        return builder.GetEdmModel();
    }

    public static IEdmModel AirVinylModel => _model ??= GetAirVinylEntityDataModel();
}
