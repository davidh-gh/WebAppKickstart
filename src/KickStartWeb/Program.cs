using Aspire.ServiceDefaults;
using Core.Utils.HealthCheck;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.WebHost.UseKestrel(options =>
{
    options.AddServerHeader = false; // Disable the Server header for security reasons (default: Kestrel)
});
builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});

var memCollection = new List<KeyValuePair<string, string?>>
{
    new("MySettings:Setting1", "Settings from memory collection"),
    new("Aspire:ServiceDefaults:DefaultUICulture", "en-US"),
    new("Aspire:ServiceDefaults:DefaultTimeZone", "UTC"),
    new("Aspire:ServiceDefaults:DefaultDateFormat", "MM/dd/yyyy"),
    new("Aspire:ServiceDefaults:DefaultTimeFormat", "HH:mm:ss")
};
builder.Configuration.AddInMemoryCollection(memCollection);

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("api", options =>
{
    var baseUrl = builder.Configuration["Api:BaseUrl"];
    options.BaseAddress = new Uri(baseUrl!);
    options.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHealthChecks()
    .AddCheck<FakeRandomHealthCheck>("Web Site Health Check")
    .AddCheck<FakeRandomHealthCheck>("Web Database Health Check");

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapHealthChecks("/health").AllowAnonymous();
app.MapHealthChecks("/health-diag", new HealthCheckOptions
{
    ResponseWriter = UiResponseWriter.WriteHealthCheckUiResponse
}).AllowAnonymous();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


await app.RunAsync().ConfigureAwait(false);
