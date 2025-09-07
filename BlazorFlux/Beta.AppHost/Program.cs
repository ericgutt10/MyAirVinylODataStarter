var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Beta_ApiService>("apiservice");

builder.AddProject<Projects.Beta_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
