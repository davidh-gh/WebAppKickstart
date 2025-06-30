using Asp.Versioning;
using Aspire.ServiceDefaults;

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync().ConfigureAwait(false);