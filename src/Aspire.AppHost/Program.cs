var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.KickStartWeb>("kickstartweb");

builder.Build().Run();
