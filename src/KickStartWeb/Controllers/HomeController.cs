using Core.Info;
using KickStartWeb.Models;
using KickStartWeb.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KickStartWeb.Controllers;

public class HomeController(ILogger<HomeController> logger, IConfiguration config, IHttpClientFactory clientFactory) : Controller
{
    public IActionResult Index()
    {
        // simple way of accessing configuration settings
        ViewData["MySetting1"] = config["MySettings:Setting1"];
        ViewData["MySetting2"] = config["MySettings:Setting2"];

        // better way of accessing configuration settings
        ViewData["MyApp"] = config.GetSection("MyAppSettings").Get<MyAppSettingsOptions>();

        LogIndexPageHello(logger, null);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private static readonly Action<ILogger, Exception?> LogIndexPageHello =
        LoggerMessage.Define(LogLevel.Information, new EventId(1, nameof(Index)), "Index page says hello");

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var client = clientFactory.CreateClient("api");

        try
        {
            var response = await client.PostAsJsonAsync("api/v2/Authentication/token", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (result is null || string.IsNullOrWhiteSpace(result.Token))
            {
                ModelState.AddModelError(string.Empty, "Login failed. No token returned.");
                return View(model);
            }

            HttpContext.Session.SetString("AccessToken", result.Token);
            return RedirectToAction("Index", "Home");
        }
#pragma warning disable CA1031
        catch (Exception ex)
#pragma warning restore CA1031
        {
#pragma warning disable CA1848
            logger.LogError(ex, "Login failed");
#pragma warning restore CA1848
            ModelState.AddModelError(string.Empty, "An error occurred during login.");
            return View(model);
        }
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("AccessToken");
        return RedirectToAction("Login");
    }
}