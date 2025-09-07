using System.Diagnostics.CodeAnalysis;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Code.Monitor;
using WebApi.Controllers.V1;

namespace WebApi.Controllers;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
[ApiController]
[ApiVersionNeutral]
[Route("api/[controller]")]
internal sealed class HealthController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    // the url for this endpoint is: /api/Health
    [EndpointSummary("Get Health")]
    [EndpointDescription("Description of the Get Health endpoint.")]
    [Tags("Health")]
    [HttpGet]
    [Route("ping")]
    [AllowAnonymous]
    public IActionResult Index()
    {
        LogMessages.LogHealthPing(logger, null);
        return Ok();
    }
}
