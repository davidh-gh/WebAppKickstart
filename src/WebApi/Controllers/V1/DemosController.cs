using Microsoft.AspNetCore.Mvc;
using WebApi.Code.Monitor;

namespace WebApi.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class DemosController(ILogger<DemosController> logger) : ControllerBase
{
    // GET - retrieve record or list of records
    // POST - create record
    // PUT - update whole record
    // PATCH - partially update record
    // DELETE - delete record

    // GET: api/<Demos>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        LogMessages.LogDemosGet(logger, null);
        return ["value1", "value2"];
    }

    // GET api/<Demos>/id/5
    [HttpGet("id/{id}")]
    public IActionResult GetById(int id)
    {
        if(id is < 0 or > 100)
        {
            LogMessages.LogDemosGetByIdInvalidId(logger, id, null);
            return BadRequest("Id must be between 0 and 100");
        }

        LogMessages.LogDemosGetById(logger, id, null);
        return Ok($"Value {id}");
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