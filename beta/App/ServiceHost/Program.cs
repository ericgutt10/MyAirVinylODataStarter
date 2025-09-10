using AirVinyContext.Entities;
using App.AirVinyl.Module3.Services;
using App.AirVinyl.Module3.Validation;
using App.ServerHost.CustomFilters;
using Carter;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using static App.AirVinyl.Module3.EdmModel.Module3ModelBuilder;

// For JSON Seriaization Policy
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.IncludeFields = true;
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOData(q => q.EnableAll());

builder.Services.AddModule3ApiServices<Program>();

builder.Services.AddCarter();

var app = builder.Build();

//ODataMiniMetadata me = new ODataMiniMetadata();
//me.Services = services =>
//{
//    services.AddSingleton<IFilterBinder, FilterBinder>();
//};

app.MapCarter();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseODataRouteDebug();
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.MapGet("/getpeople1", (MyAirVinylCtx ctx) =>
{
    return ctx.People.AsNoTracking().Include(nameof(ctx.VinylRecords));
})
    .WithODataModel(AirVinylMod3Model)
    .WithODataResult();

app.MapGet("/getpeople2", (MyAirVinylCtx ctx, ODataQueryOptions<Person> options) =>
{
    return options.ApplyTo(ctx.People);
}).WithODataResult();

app.MapGet("/getvinyl", (MyAirVinylCtx ctx) =>
{
    return ctx.VinylRecords;
})
    .WithODataResult();

app.Run();