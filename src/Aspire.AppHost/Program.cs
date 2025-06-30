var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.KickStartWeb>("kickstartweb");

builder.AddProject<Projects.WebApi>("webapi")
    .WithUrl("swagger", "Swagger");

await builder.Build().RunAsync();