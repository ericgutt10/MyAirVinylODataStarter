var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Delta_ApiService>("apiservice");

builder.AddProject<Projects.Delta_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
