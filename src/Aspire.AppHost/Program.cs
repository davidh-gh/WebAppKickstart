using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var webapi = builder.AddProject<Projects.WebApi>("webapi")
    .WithUrl("swagger", "Swagger");

builder.AddProject<Projects.KickStartWeb>("kickstartweb")
    .WithReference(webapi);

var webApiUrl = webapi.GetEndpoint("https");

// add to environment variables
builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
{
    ["Api:BaseUrl"] = webApiUrl.ToString()
});

await builder.Build().RunAsync();