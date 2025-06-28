# How to setup API

## To have open API documentation, add the following to your `Program.cs`:

```csharp
builder.Services.AddOpenApi();
// to swap from /openapi/v1.json to /v1/v1.json; but can leave it to default: /openapi/v1.json
app.MapOpenApi("{documentName}/v1.json");
```

## To have OpenAPI UI:
* add Microsoft.Extensions.ApiDescription.Server package
* to project file add:
```xml
<PropertyGroup>
    <OpenApiDocumentsDirectory>.</OpenApiDocumentsDirectory>
    <OpenApiGenerateDocumentsOptions>--file-name open-api</OpenApiGenerateDocumentsOptions>
</PropertyGroup>
```
* add Swashbuckle.AspNetCore.SwaggerUi package
* and configure Swagger UI in `Program.cs`:
```csharp
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});
```
* launch the app and navigate to /swagger to view the Swagger UI.
