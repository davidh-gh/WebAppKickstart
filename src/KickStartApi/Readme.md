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

Dont forget to add app.MapOpenApi().AllowAnonymous();

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
