# How to set up dedicated Web Health UI project

# Health Checks in outside web project
To add health checks, you can use the `Microsoft.AspNetCore.Diagnostics.HealthChecks` package. Here’s how to set it up:
```csharp
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy("The service is healthy"))
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), name: "Database");
// Add other health checks as needed
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
```
This will add a health check endpoint at `/health` that returns the health status of your application and its dependencies.

And also implement with IHealthCheck interface the things you want to check, e.g., database connection, external service availability, etc.
```csharp
builder.Services.AddHealthChecks()
.AddCheck<MyCustomHealthCheck>("My Custom Check");
```

* Also check ServiceDefault class how is used to register health checks.
* For possible health checks see `https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks`

### Better options for health checks
Install packages `AspNetCore.HealthChecks.UI`, `AspNetCore.HealthChecks.UI.Client` and `AspNetCore.HealthChecks.UI.InMemory.Storage` to have a better UI for health checks.
Then, you can configure it in `Program.cs`:

```csharp
builder.Services.AddHealthChecksUI(opts =>
{
    opts.AddHealthCheckEndpoint("api", "/health-diag");
    opts.SetEvaluationTimeInSeconds(5); // this should be less than the minimum seconds between failure notifications and at least 60 seconds
    opts.SetMinimumSecondsBetweenFailureNotifications(10); // this should be greater than the evaluation time (like 5x evaluation time)
}).AddInMemoryStorage();

app.MapHealthChecks("/health");
app.MapHealthChecks("/health-diag", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();
```

The result summary is available at `/health`.
The result details is available at `/health-diag`.
The UI is available at `/healthchecks-ui` and provides a nice dashboard to monitor the health of your application and its dependencies. UI could be a separate project, e.g., `WebApi.HealthChecksUI`.

# OpenTelemetry

Check ServiceExtensions for OpenTelemetry setup.
Expand on that.

# Model validation

To add model validation, you can use the `FluentValidation.AspNetCore` package. Here’s how to set it up:
```csharp
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
```
Then, you can create validators for your models by implementing the `IValidator<T>` interface. For example:
```csharp
public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Email).NotEmpty().EmailAddress();
        RuleFor(user => user.Password).NotEmpty().MinimumLength(6);
    }
}
```
This will automatically validate your models when they are bound to your controllers, and return validation errors in the response if the model is invalid.

# Option 2

Create Request model in Models folder, e.g., `CreateUserRequest.cs` and within the controller method add the following code:
```csharp
[HttpPost]
public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // Your logic to create user
}
```

# CORS

To add CORS (Cross-Origin Resource Sharing) support, you can use the `Microsoft.AspNetCore.Cors` package. Here’s how to set it up:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
// Use CORS policy
app.UseCors("AllowAllOrigins");
```
This will allow all origins, methods, and headers. You can customize the policy to restrict it to specific origins, methods, or headers as needed.

# Rate Limiting

The RateLimiterOptionsExtensions class provides the following extension methods for rate limiting:

    * Fixed window
    * Sliding window
    * Token bucket
    * Concurrency

Here’s how to set it up:
```csharp
builder.Services.AddRateLimiter(options =>
{
    // first global rate limiter is checked (so allow more), then the policy rate limiter on the controller
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

app.UseRateLimiter();
```
This will limit the number of requests from each IP address to 100 requests per minute. You can customize the `PermitLimit` and `Window` values as needed.

# Data Caching

To add caching, you can use the `Microsoft.Extensions.Caching.Memory` package. Here’s how to set it up:
```csharp
builder.Services.AddMemoryCache();
```
Then, you can use the `IMemoryCache` interface to cache data in your controllers or services. For example:
```csharp
public class UserService
{
    private readonly IMemoryCache _cache;

    public UserService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<User> GetUserAsync(int userId)
    {
        if (!_cache.TryGetValue(userId, out User user))
        {
            // Simulate fetching user from database
            user = await FetchUserFromDatabaseAsync(userId);
            _cache.Set(userId, user, TimeSpan.FromMinutes(5)); // Cache for 5 minutes
        }
        return user;
    }

    private Task<User> FetchUserFromDatabaseAsync(int userId)
    {
        // Simulate database call
        return Task.FromResult(new User { Id = userId, Name = "John Doe" });
    }
}
```

# Response Caching

To add response caching, you can use the `Microsoft.AspNetCore.ResponseCaching` package. Here’s how to set it up:
```csharp
builder.Services.AddResponseCaching();
app.UseResponseCaching();
```
Then, you can use the `[ResponseCache]` attribute on your controller actions to specify caching behavior. For example:
```csharp
[HttpGet]
[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
public async Task<IActionResult> GetUser(int userId)
{
    var user = await _userService.GetUserAsync(userId);
    return Ok(user);
}
```
This will cache the response for 60 seconds, allowing subsequent requests to be served from the cache instead of hitting the database.

Cache is usable only on Get requests, and it is not suitable for POST, PUT, DELETE requests.


# Example of order of middleware in `Program.cs`:
```csharp
if (!app.Environment.IsDevelopment())
{
    // Use HSTS in production
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseRouting();
app.UseRateLimiter();
app.UseRequestLocalization();
app.UseCors(policy =>
{
     policy.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();
});

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();
app.UseResponseCompression();
app.UseResponseCaching();


app.MapHealthChecks("/health");
app.MapHealthChecks("/health-diag", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();

app.MapControllers();

await app.RunAsync().ConfigureAwait(false);
```
