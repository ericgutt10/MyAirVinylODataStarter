using AirVinyContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using static App.AirVinyl.Module3.EdmModel.Module3ModelBuilder;
using static App.AirVinyl.Module3.Handlers.Module3Handlers;

namespace App.AirVinyl.Module3.Endpoints;

public class Module3Endpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ODataMessageWriterSettings settings = new ODataMessageWriterSettings
        {
            EnableMessageStreamDisposal = false,
            MessageQuotas = new ODataMessageQuotas { MaxReceivedMessageSize = Int64.MaxValue },
        };
        settings.SetOmitODataPrefix(false);

        var odataGroup = app.MapGroup("odata").WithOpenApi();

        odataGroup.MapODataServiceDocument("$document", AirVinylMod3Model);

        odataGroup.MapODataMetadata("$metadata", AirVinylMod3Model);

        odataGroup.MapGet("/People", (MyAirVinylCtx ctx) => FuncExpr.ReturnQuery(GetPeople, ctx))
            .WithODataResult()
            ;


        odataGroup.MapGet("/People/Records", (MyAirVinylCtx ctx) => FuncExpr.ReturnQuery(GetPeopleRecords, ctx))
            .WithODataModel(AirVinylMod3Model)
            .WithODataResult()
        ;

        odataGroup.MapGet("/People({key})", (MyAirVinylCtx ctx, int key) => FuncExpr.ReturnSingle(GetPerson, ctx, key))
            .WithODataResult();

        odataGroup.MapGet("/People({key})/Records",
                async (MyAirVinylCtx ctx, int key) => await FuncExpr.ReturnSingleAsync(GetPersonRecordsAsync, ctx, key))
            .WithODataPathFactory((h, t) =>
            {
                IEdmEntitySet people = AirVinylMod3Model.FindDeclaredEntitySet("People");
                return new ODataPath(new EntitySetSegment(people));
            })
            .WithODataModel(AirVinylMod3Model)
            .WithODataResult()
        ;

        odataGroup.MapGet("/Records",
            async (MyAirVinylCtx ctx) => await FuncExpr.ReturnQueryAsync(GetRecordsAsync, ctx))
            .WithODataResult()
            ;
    }
}

public static class FuncExpr
{

    public static object ReturnSingle<TCtx, TDbSet>(this
        Func<TCtx, int, TDbSet?> func,
        TCtx ctx,
        int key)
    {
        return func.DynamicInvoke([ctx, key]) switch
        {
            TDbSet p => p,
            _ => Results.NotFound()
        };
    }

    public static async Task<object?> ReturnSingleAsync<TCtx, TDbSet>(this
        Func<TCtx, int, Task<TDbSet?>> func,
        TCtx ctx,
        int key)
    {
        TDbSet? res = (await Task.Run(() => func.DynamicInvoke([ctx, key])))
                is not Task<TDbSet?> tsk ? default : await tsk;

        return res switch
        {
            TDbSet dbset => dbset,
            _ => Results.NotFound()
        };

    }

    public static object ReturnQuery<TCtx, TDbSet>(this
        Func<TCtx, IQueryable<TDbSet>?> func,
        TCtx ctx
        )
    {
        return func.DynamicInvoke([ctx]) switch
        {
            IQueryable<TDbSet> p => p,
            _ => Results.NoContent()
        };
    }

    public static async Task<object?> ReturnQueryAsync<TCtx, TDbSet>(this
        Func<TCtx, Task<IQueryable<TDbSet>?>> func,
        TCtx ctx
        )
    {
        IQueryable<TDbSet>? res = (await Task.Run(() => func.DynamicInvoke([ctx])))
                is not Task<IQueryable<TDbSet>?> tsk ? default : await tsk;

        return res switch
        {
            IQueryable<TDbSet> dbset => dbset,
            _ => Results.NotFound()
        };

    }
}