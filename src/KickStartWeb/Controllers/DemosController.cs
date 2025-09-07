using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KickStartWeb.Controllers;

internal sealed class DemosController(IHttpClientFactory clientFactory) : Controller
{
    [HttpGet]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
    [EnableRateLimiting("fixedPolicy")]
    public async Task<IActionResult> GetList()
    {
        var userToken = HttpContext.Session.GetString("AccessToken");
        if (string.IsNullOrEmpty(userToken))
        {
            RedirectToAction("Login", "Home");
        }

        var client = clientFactory.CreateClient("api");
        var list = await client.GetFromJsonAsync<IEnumerable<string>>("api/v2/Demos").ConfigureAwait(false) ?? [];

        return Ok(list);
    }

    [HttpGet]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)] // this applies to unique ids
    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (id is < 0 or > 100)
        {
            // Log the invalid ID request
            return BadRequest("Id must be between 0 and 100");
        }
        var userToken = HttpContext.Session.GetString("AccessToken");
        if (string.IsNullOrEmpty(userToken))
        {
            RedirectToAction("Login", "Home");
        }

        var client = clientFactory.CreateClient("api");
        var result = await client.GetFromJsonAsync<string>($"api/v2/Demos/id/{id}").ConfigureAwait(false);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string value)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userToken = HttpContext.Session.GetString("AccessToken");
        if (string.IsNullOrEmpty(userToken))
        {
            RedirectToAction("Login", "Home");
        }

        var client = clientFactory.CreateClient("api");
        var result = await client.PostAsJsonAsync("api/v2/Demos", value).ConfigureAwait(false);

        if (result.IsSuccessStatusCode)
        {
            return Ok($"Value {value}");
        }

        return NoContent();
    }

    [HttpPost]
    public IActionResult Update(int id, [FromBody] string value)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok($"Value {value}");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok($"Value {id}");
    }
}
