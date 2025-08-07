using Asp.Versioning;
using Domain.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using WebApi.Code.Monitor;

namespace WebApi.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SitesController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private static readonly Action<ILogger, Exception?> LogGetSite =
        LoggerMessage.Define(LogLevel.Information, new EventId((int)LogEvents.None, "GetSite"), "Get weather forecast for days");

    // the url for this endpoint is: /site/GetSite
    [EndpointSummary("Get Site")]
    [EndpointDescription("Description of the Get Site endpoint.")]
    [Tags("Site")]
    [HttpGet(Name = "GetSiteV1")] // this should reflect /sites/GetSite, but it does not
    // [Authorize(Roles = "Admin,User")] // Use the Roles attribute to specify required roles
    // [Authorize(Policy = "MustHaveUserRole")] // Use the Policy attribute to specify a policy
    [ProducesResponseType<UserDb>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Index()
    {
        LogGetSite(logger, null);
        return Ok(new UserDb());
    }
}