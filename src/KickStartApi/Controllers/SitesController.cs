using KickStartApi.Code;
using Microsoft.AspNetCore.Mvc;

namespace KickStartApi.Controllers;

// Use Plural naming for controller names, e.g., SitesController
[ApiController]
[Route("[controller]")]
public class SitesController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private static readonly Action<ILogger, Exception?> LogGetSite =
        LoggerMessage.Define(LogLevel.Information, new EventId((int)LogEvents.None, "GetSite"), "Get weather forecast for days");

    // the url for this endpoint is: /site/GetSite
    [EndpointSummary("Get Site")]
    [EndpointDescription("Description of the Get Site endpoint.")]
    [Tags("Site")]
    [HttpGet(Name = "GetSite")] // this should reflect /sites/GetSite
    public IResult Index()
    {
        LogGetSite(logger, null);
        return Results.Ok();
    }
}