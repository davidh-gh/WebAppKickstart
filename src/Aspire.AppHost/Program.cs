using Microsoft.Extensions.Configuration;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var webapi = builder.AddProject<WebApi>("webapi")
    .WithUrl("swagger", "Swagger");

builder.AddProject<KickStartWeb>("kickstartweb")
    .WithReference(webapi);

var webApiUrl = webapi.GetEndpoint("https");

// add to environment variables
builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
{
    ["Api:BaseUrl"] = webApiUrl.ToString()
});

builder.AddProject<HealthUIWeb>("healthuiweb")
    .WithUrl("health", "Health")
    .WithUrl("health-diag", "HealthDiag")
    .WithUrl("healthchecks-ui", "HealthChecksUI");

await builder.Build().RunAsync().ConfigureAwait(false);