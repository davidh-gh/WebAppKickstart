var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.KickStartWeb>("kickstartweb");

builder.AddProject<Projects.KickStartApi>("kickstartapi");

await builder.Build().RunAsync();