using Aspire.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

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

builder.Services.AddHttpClient("api", options=>
{
    var baseUrl = builder.Configuration["Api:BaseUrl"];
    options.BaseAddress = new Uri(baseUrl!);
    options.DefaultRequestHeaders.Add("Accept", "application/json");
});

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

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


await app.RunAsync();