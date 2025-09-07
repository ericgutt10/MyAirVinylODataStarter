using AirVinyContext.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace MyAirVinylMiniCarter.UseCases.AirVinyl.Handlers;

public static class AirVinylHandlers
{
    public static async Task<Results<InternalServerError, Ok<List<Person>>>>
    //  GetPeopleAsync(IRepository<Person> repo, ILogger<Person>? logger, CancellationToken cancellation)
    GetPeopleAsync(MyAirVinylCtx ctx, ILogger<Person>? logger, CancellationToken cancellation)
    {
        try
        {
            return TypedResults.Ok(await ctx.People.ToListAsync(cancellation));
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Exception in {MethodName}", nameof(GetPeopleAsync));
            throw;
        }
    }
}