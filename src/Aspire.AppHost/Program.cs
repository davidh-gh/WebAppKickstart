var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.KickStartWeb>("kickstartweb");

await builder.Build().RunAsync();