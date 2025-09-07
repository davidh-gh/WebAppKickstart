using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using System.Security.Cryptography;
using WebApi.Code.Monitor;

namespace WebApi.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
internal sealed class DemosController(ILogger<DemosController> logger) : ControllerBase
{
    // GET - retrieve record or list of records
    // POST - create record
    // PUT - update whole record
    // PATCH - partially update record
    // DELETE - delete record

    // GET: api/<Demos>
    [HttpGet]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
    [EnableRateLimiting("fixedPolicy")]
    public IEnumerable<string> Get()
    {
        LogMessages.LogDemosGet(logger, null);

        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; // Get the user ID from the claims, which is saved as JwtRegisteredClaimNames.Sub

        var nbr = RandomNumberGenerator.GetInt32(1, 10);

        var values = new List<string>();
        for (int i = 0; i < nbr; i++)
        {
            values.Add($"value {i + 1} for user {userId}");
        }

        return values;
    }

    // GET api/<Demos>/id/5
    [HttpGet("id/{id}")]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)] // this applies to unique ids
    public IActionResult GetById(int id)
    {
        if (id is < 0 or > 100)
        {
            LogMessages.LogDemosGetByIdInvalidId(logger, id, null);
            return BadRequest("Id must be between 0 and 100");
        }

        LogMessages.LogDemosGetById(logger, id, null);

        var nbr = RandomNumberGenerator.GetInt32(1, 100);

        return Ok($"Value[{nbr}]: {id}");
    }

    // POST api/<Demos>
    [HttpPost]
    public IActionResult Post([FromBody] string value)
    {
        return Ok($"Value {value}");
    }

    // PUT api/<Demos>/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] string value)
    {
        return Ok($"Value {value}");
    }

    // PATCH api/<Demos>/5
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, [FromBody] string value)
    {
        return Ok($"Value {value}");
    }

    // DELETE api/<Demos>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok($"Value {id}");
    }
}
