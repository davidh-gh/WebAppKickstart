using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy("The service is healthy"));

builder.Services.AddHealthChecksUI(opts =>
{
    opts.AddHealthCheckEndpoint("HealthUI", "/health-diag");
    opts.SetEvaluationTimeInSeconds(60); // this should be less than the minimum seconds between failure notifications and at least 60 seconds
    opts.SetMinimumSecondsBetweenFailureNotifications(60 * 5); // this should be greater than the evaluation time (like 5x evaluation time)
    opts.SetNotifyUnHealthyOneTimeUntilChange();
    // opts.AddWebhookNotification() // send to webhook - i.e. send to Slack, Teams, Email, etc.
    //  setup.AddCustomStylesheet("dotnet.css"); // add custom stylesheet
}).AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapHealthChecks("/health").AllowAnonymous();
app.MapHealthChecks("/health-diag", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}).AllowAnonymous();

app.MapHealthChecksUI();

await app.RunAsync().ConfigureAwait(false);