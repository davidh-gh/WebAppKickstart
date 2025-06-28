using Microsoft.AspNetCore.Mvc;

namespace KickStartApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DemosController : ControllerBase
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
        return ["value1", "value2"];
    }

    // GET api/<Demos>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<Demos>
    [HttpPost]
    public void Post([FromBody] string value)
    {
        // ignored
    }

    // PUT api/<Demos>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
        // ignored
    }

    // PATCH api/<Demos>/5
    [HttpPatch("{id}")]
    public void Patch(int id, [FromBody] string value)
    {
        // ignored
    }

    // DELETE api/<Demos>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        // ignored
    }
}