var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.KickStartWeb>("kickstartweb");

builder.AddProject<Projects.KickStartApi>("kickstartapi")
    .WithUrl("swagger", "Swagger");

await builder.Build().RunAsync();