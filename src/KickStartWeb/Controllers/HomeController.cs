using KickStartWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KickStartWeb.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    public IActionResult Index()
    {
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

}