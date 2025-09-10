using AirVinyContext.Entities;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace App.AirVinyl.Module3.EdmModel;

public static class Module3ModelBuilder
{
    private static IEdmModel? _model;

    private static IEdmModel GetAirVinylEntityDataModel()
    {
        var builder = new ODataConventionModelBuilder();
        // Define entity sets
        builder.EntitySet<Person>($"People");
        builder.ComplexType<VinylRecord>();

        //builder.EntitySet<VinylRecord>($"{nameof(VinylRecord)}s");
        //builder.EntitySet<PressingDetail>($"{nameof(PressingDetail)}s");
        //builder.EntitySet<Rating>($"{nameof(Rating)}s");
        //builder.EntitySet<RecordStore>($"{nameof(RecordStore)}s");

        return builder.GetEdmModel();
    }

    public static IEdmModel AirVinylMod3Model => _model ??= GetAirVinylEntityDataModel();
}