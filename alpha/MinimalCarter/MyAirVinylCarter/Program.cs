using AirVinyContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;
using MyAirVinylCarter;
using MyAirVinylCarter.Lib;
using Microsoft.AspNetCore.OData;

using static Microsoft.AspNetCore.OData.ODataServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

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

IEdmModel model = EdmModelBuilder.GetEdmModel();

builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.IncludeFields = true;
});

builder.Services.AddOData(q => q.EnableAll());


builder.Services.AddCarter();

// In pre-built OData, MailAddress and Student are complex types

// Learn more about configuring Swagger/OpenAPI at https://github.com/OData/AspNetCoreOData/tree/main/sample/ODataRoutingSample
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AirVinylODataApp", Version = "v1" });
});

// OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapCarter();

app.Run();