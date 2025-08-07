using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HealthUIWeb.Models;
using System.Diagnostics.CodeAnalysis;

namespace HealthUIWeb.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    [SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates")]
    public IActionResult Index()
    {
        logger.LogInformation("Index");
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
}