using KickStartApi.Code;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace KickStartApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private static readonly string[] Summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];
    private static readonly Action<ILogger, int, Exception?> LogWeatherForecastRequestDelegate =
        LoggerMessage.Define<int>(LogLevel.Debug,
            new EventId((int)LogEvents.WeatherForecastGet, "GetWeatherForecast"),
            "Get weather forecast for {Count} days");

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        LogWeatherForecastRequestDelegate(logger, RandomNumberGenerator.GetInt32(int.MaxValue), null);
        return [.. Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = RandomNumberGenerator.GetInt32(-20, 55),
                Summary = Summaries[RandomNumberGenerator.GetInt32(Summaries.Length)]
            })];
    }
}