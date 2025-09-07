using AirVinyContext.Entities;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace MyAirVinyl.EnitityDataModel;

public partial class EdmModelBuilder
{
    public static IEdmModel GetAirVinylEntityDataModel()
    {
        var builder = new ODataConventionModelBuilder
        {
            Namespace = "AirVinyl",
            ContainerName = "AirVinylContainier"
        };
        // Define entity sets
        builder.EntitySet<Person>("Persons");
        builder.EntitySet<VinylRecord>("VinylRecords");

        //builder.EntitySet<AirVinyContext.Entities.RecordStore>("RecordStores");
        //builder.EntitySet<AirVinyContext.Entities.Rating>("Ratings");
        //builder.EntitySet<AirVinyContext.Entities.PressingDetail>("PressingDetails");
        //builder.EntitySet<AirVinyContext.Entities.lib.SpecializedRecordStore>("SpecializedRecordStores");

        // Define complex types if any (none in this case)
        // Define actions and functions if any (none in this case)

        return builder.GetEdmModel();
    }
}
