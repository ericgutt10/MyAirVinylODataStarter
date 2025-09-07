using AirVinyContext.Entities;
using AirVinylRepository;
using Carter;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using MyAirVinylMiniCarter.Lib;
using MyAirVinylMiniCarter.UseCases.AirVinyl.EdmModel;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(lc =>
{
    lc.Logger<Program>(builder.Configuration);
});

IEdmModel model = EdmModelBuilder.AirVinylModel;
builder.Services.AddSingleton(model);
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddDbContext<MyAirVinylCtx>(opt =>
{
    var (connection, retry, timeout) = builder.Services.GetDbConfig("AppSettings:ConnectionName");

    opt.UseSqlServer(connection,
         opt =>
         {
             opt.EnableRetryOnFailure(retry);
             opt.CommandTimeout(timeout);
         });
});

builder.Services.AddOData(q => q.EnableAll());

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCarter();

builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

ODataMiniMetadata me = new()
{
    Services = services =>
    {
        services.AddSingleton<IFilterBinder, FilterBinder>();
    }
};



app.UseHttpsRedirection();

app.MapCarter();

app.Run();



/// <summary>
/// This is required in E2E test to identify the assembly.
/// </summary>
public partial class Program
{ }