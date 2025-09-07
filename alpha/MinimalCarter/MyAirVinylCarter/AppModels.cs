using AirVinyContext.Entities;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace MyAirVinylCarter;

public class EdmModelBuilder
{
    public static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();
        // Define entity sets
        builder.EntitySet<Person>("Persons");
        builder.EntitySet<VinylRecord>("VinylRecords");


        return builder.GetEdmModel();
    }
}