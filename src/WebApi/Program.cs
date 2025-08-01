using Asp.Versioning;
using Aspire.ServiceDefaults;
using Core.Utils.HealthCheck;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.RateLimiting;
using System.Net;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true; // Include API version in response headers
    options.AssumeDefaultVersionWhenUnspecified = true; // Use default version if not specified
    options.DefaultApiVersion = new ApiVersion(1, 0); // Set default API version
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // Format for versioned API groups
    options.SubstituteApiVersionInUrl = true; // Substitute API version in URL
});


builder.Services.AddHealthChecks()
    .AddCheck<FakeRandomHealthCheck>("Api Site Health Check")
    .AddCheck<FakeRandomHealthCheck>("Api Database Health Check");

builder.Services.AddResponseCaching(options =>
{
    options.UseCaseSensitivePaths = false; // Enable case-sensitive paths for caching
    options.SizeLimit = 1024 * 1024 * 1000; // Set maximum body size for cached responses (1 MB)
});

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
        context => RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100, // Maximum number of requests
                Window = TimeSpan.FromMinutes(1) // Time window for rate limiting
            }));
    options.AddFixedWindowLimiter(policyName: "fixedPolicy", fixedWindowRateLimiterOptions =>
    {
        fixedWindowRateLimiterOptions.PermitLimit = 2;
        fixedWindowRateLimiterOptions.Window = TimeSpan.FromSeconds(10);
        fixedWindowRateLimiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        fixedWindowRateLimiterOptions.QueueLimit = 2;
        fixedWindowRateLimiterOptions.AutoReplenishment = true;
    });
    options.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // to swap definition path from /openapi/v1.json to /v1/v1.json; but can leave it to default: /openapi/v1.json
    app.MapOpenApi().AllowAnonymous();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v2.json", "v2"); // Swagger endpoint for version 2
        options.SwaggerEndpoint("/openapi/v1.json", "v1"); // Swagger endpoint for version 1
    });
}

if (!app.Environment.IsDevelopment())
{
    // Use HSTS in production
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.UseResponseCaching();

app.MapHealthChecks("/health").AllowAnonymous();
app.MapHealthChecks("/health-diag", new HealthCheckOptions
{
    ResponseWriter = UiResponseWriter.WriteHealthCheckUiResponse
}).AllowAnonymous();

app.MapControllers();

await app.RunAsync().ConfigureAwait(false);