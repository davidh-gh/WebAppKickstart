using Aspire.ServiceDefaults;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Document name is v1, which can be customized if needed
// Available at /openapi/v1.json
builder.Services.AddOpenApi();

builder.Services.AddAuthorization(options=>
{
    options.AddPolicy("MustHaveUserRole", policy =>
    {
        policy.RequireClaim("employeeId");
        policy.RequireRole("User");
    });
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("Authentication:Authority");
        options.Audience = builder.Configuration.GetValue<string>("Authentication:Audience");
        options.IncludeErrorDetails = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(
                    builder.Configuration.GetValue<string>("Authentication:SecretKey") ?? throw new ArgumentException("SecretKey is missing in configuration.")))
        };

        options.MapInboundClaims = true;
        options.Validate(JwtBearerDefaults.AuthenticationScheme);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // to swap definition path from /openapi/v1.json to /v1/v1.json; but can leave it to default: /openapi/v1.json
    app.MapOpenApi().AllowAnonymous();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync().ConfigureAwait(false);