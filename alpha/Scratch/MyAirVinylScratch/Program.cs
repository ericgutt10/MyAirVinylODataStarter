using AirVinyContext.Entities;
using AirVinylRepository;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using MyAirVinylScratch.Lib;

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Person>("Person");
    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddOData(options => options
        .AddRouteComponents("odata", GetEdmModel())
        .Select()
        .Filter()
        .OrderBy()
        .SetMaxTop(20)
        .Count()
        .Expand()
    );
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContextFactory<MyAirVinylCtx>(opt =>
{
    var (connection, retry, timeout) = builder.Services.GetDbConfig("AppSettings:ConnectionName");

    opt.UseSqlServer(connection,
         opt =>
         {
             opt.EnableRetryOnFailure(retry);
             opt.CommandTimeout(timeout);
         });
});

builder.Services.AddScoped<IAirVinylRepo, AirVinylRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseODataRouteDebug();

    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();