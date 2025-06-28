using Aspire.ServiceDefaults;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Document name is v1, which can be customized if needed
// Available at /openapi/v1.json
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // to swap definition path from /openapi/v1.json to /v1/v1.json; but can leave it to default: /openapi/v1.json
    app.MapOpenApi();
    app.MapOpenApi("/{documentName}/v1.json");

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "KickStart API V1");
        options.SwaggerEndpoint("/v1/v1.json", "KickStart API V2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();