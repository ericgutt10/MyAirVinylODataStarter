using CustomersODataApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

// Learn more about configuring OData at https://learn.microsoft.com/odata/webapi-8/getting-started
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddOData(opt =>
{
    DefaultODataBatchHandler defaultBatchHandler = new DefaultODataBatchHandler();
    defaultBatchHandler.MessageQuotas.MaxNestingDepth = 2;
    defaultBatchHandler.MessageQuotas.MaxOperationsPerChangeset = 10;
    defaultBatchHandler.MessageQuotas.MaxReceivedMessageSize = 100;

    opt.AddRouteComponents(
        routePrefix: "odata",
        model: EdmModelBuilder.GetEdmModel(),
        batchHandler: defaultBatchHandler)
       .EnableQueryFeatures(100);

    opt.RouteOptions.EnableControllerNameCaseInsensitive = true;
    opt.RouteOptions.EnableActionNameCaseInsensitive = true;
    opt.RouteOptions.EnablePropertyNameCaseInsensitive = true;
    opt.RouteOptions.EnableNonParenthesisForEmptyParameterFunction = true;
});

// Learn more about configuring Swagger/OpenAPI at https://github.com/OData/AspNetCoreOData/tree/main/sample/ODataRoutingSample
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomersODataApp", Version = "v1" });
});

// OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseODataBatching();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseODataRouteDebug();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomersODataApp V1"));

    // OpenAPI
    app.MapOpenApi();
}

app.UseRouting();

app.MapControllers();

app.Run();
