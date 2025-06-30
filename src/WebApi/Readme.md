# How to set up API

**Note:** 
Use Plural naming for controller names, e.g., *SitesController, UsersController*, etc.

Do not use package *Microsoft.IdentityModel.Tokens*

## To have open API documentation, add the following to your `Program.cs`:

```csharp
builder.Services.AddOpenApi();
// to swap from /openapi/v1.json to /v1/v1.json; but can leave it to default: /openapi/v1.json
app.MapOpenApi("{documentName}/v1.json");
```

## To have OpenAPI UI:
* add *Microsoft.Extensions.ApiDescription.Server* package
* to project file add:
```xml
<PropertyGroup>
    <OpenApiDocumentsDirectory>.</OpenApiDocumentsDirectory>
    <OpenApiGenerateDocumentsOptions>--file-name open-api</OpenApiGenerateDocumentsOptions>
</PropertyGroup>
```
* add *Swashbuckle.AspNetCore.SwaggerUI* package
* and configure Swagger UI in `Program.cs`:
```csharp
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});
```
* launch the app and navigate to /swagger to view the Swagger UI.

## To add authentication
```csharp
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(
                    builder.Configuration.GetValue<string>("Authentication:SecretKey") ?? throw new ArgumentException("SecretKey is missing in configuration.")))
        };
    });

// order is important, authentication must be before authorization
app.UseAuthentication();

app.UseAuthorization();

```

## To add authorization

Dont forget to add *app.MapOpenApi().AllowAnonymous()*

```csharp
builder.Services.AddAuthorization(options=>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});
// and disable on Authorize method with [AllowAnonymous] attribute
```

### To add custom policies
```csharp
builder.Services.AddAuthorization(options=>
{
    options.AddPolicy("MustHaveUserRole", policy =>
    {
        policy.RequireClaim("employeeId");
        policy.RequireRole("User");
    });
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});

// and use it in controller
[Authorize(Roles = "Admin,User")] // Use the Roles attribute to specify required roles
[Authorize(Policy = "MustHaveUserRole")] // Use the Policy attribute to specify a policy
[AllowAnonymous] // Use this attribute to allow anonymous access
[Authorize] // Use this attribute to require authentication
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // Use this attribute to specify the authentication scheme
```

# API Versioning
To add API versioning, you can use the `Asp.Versioning.Mvc` and `Asp.Versioning.Mvc.ApiExplorer` package (second is for swagger to work). Here’s 
how to set it up:
```csharp
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

// this works as API, but not for Swagger
```

Then, you can specify the API version in your controllers:

It is recommended to put controller in a version folder, e.g., `Controllers/V1/UsersController.cs` and use the following code in your controller:

```csharp
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    // Your actions here
}
``` 
This will allow you to version your API endpoints and provide a clear structure for versioning.
