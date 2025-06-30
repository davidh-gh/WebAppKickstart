using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApi.Code;
using WebApi.Controllers.V1;

namespace WebApi.Controllers;

[ApiController]
[ApiVersionNeutral]
[Route("api/v{version:apiVersion}/[controller]")]
public class HealthController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private static readonly Action<ILogger, Exception?> LogPing =
        LoggerMessage.Define(LogLevel.Information, new EventId((int)LogEvents.None, "Ping"), "Pinging the health endpoint");

    // the url for this endpoint is: /site/Health
    [EndpointSummary("Get Health")]
    [EndpointDescription("Description of the Get Health endpoint.")]
    [Tags("Health")]
    [HttpGet] // this should reflect /sites/GetSite, but it does not
    [Route("ping")]
    // [Authorize(Roles = "Admin,User")] // Use the Roles attribute to specify required roles
    // [Authorize(Policy = "MustHaveUserRole")] // Use the Policy attribute to specify a policy
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Index()
    {
        LogPing(logger, null);
        return Ok();
    }
}