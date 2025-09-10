using AirVinyContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App.AirVinyl.Module3.Handlers;

internal class Module3Handlers
{
    //public static //IResult
    //    //Results<ProblemHttpResult, Ok<IQueryable>>
    //    //IQueryable<Person>
    //    IQueryable
    //    //IActionResult
    //    //IIncludableQueryable<Person, IList<VinylRecord>>
    //    GetPeople(MyAirVinylCtx ctx, ODataQueryOptions<Person> queryOptions)
    //{
    //    try
    //    {
    //        var queryable =
    //        //  return new OkObjectResult()
    //         ctx.People.AsNoTracking().Include(p => p.VinylRecords);
    //        IQueryable results = queryOptions.ApplyTo(queryable, new ODataQuerySettings() /*{ EnableCorrelatedSubqueryBuffering = true}*/);
    //        return results;
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine(ex.Message);
    //        throw;
    //        //return Problem(ex.Message);
    //    }
    //}

    public static IQueryable<Person>?
    GetPeople(MyAirVinylCtx ctx)
    {
        try
        {
            return ctx.People.AsNoTracking();
               
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null;
    }

    public static IQueryable<Person>?
    GetPeopleRecords(MyAirVinylCtx ctx)
    {
        try
        {
            return ctx.People.AsNoTracking().Include(p => p.VinylRecords);

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null;
    }

    public static Person?
    GetPerson(MyAirVinylCtx ctx, int key) => ctx.People.Find(key);

    public static async Task<Person?>
    GetPersonRecordsAsync(MyAirVinylCtx ctx, int key)
    {
        try
        {
            var val = await ctx.People.AsNoTracking()
                .Include(p => p.VinylRecords)
                .FirstOrDefaultAsync(p => Equals(p.PersonId, key));
            return val;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null;
    }

    public static IQueryable<VinylRecord>
    GetRecords(MyAirVinylCtx ctx)
    {
        try
        {
            return ctx.VinylRecords;
        }
        catch
        {
            throw;
        }
    }

    public static async Task<IQueryable<VinylRecord>?>
        GetRecordsAsync(MyAirVinylCtx ctx)
    {
        try
        {
            return await Task.FromResult(ctx.VinylRecords);
        }
        catch
        {
            throw;
        }
    }
}