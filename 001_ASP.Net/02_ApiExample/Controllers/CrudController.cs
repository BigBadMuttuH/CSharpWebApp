using Microsoft.AspNetCore.Mvc;

namespace _02_ApiExample.Controllers;

[ApiController]
[Route("[controller]")]
public class CrudController : ControllerBase
{
    private static readonly Dictionary<string, string> data = new();

    [HttpPost("post")]
    public ActionResult Post(string key, string value)
    {
        try
        {
            data.Add(key, value);
            return Ok();
        }
        catch
        {
            return StatusCode(409); // конфликт 
        }
    }

    [HttpGet("get")]
    public ActionResult Get(string key)
    {
        if (data.ContainsKey(key))
            return Ok(data[key]);
        return NotFound();
    }

    [HttpPut("put")]
    public ActionResult Put(string key, string value)
    {
        if (data.ContainsKey(key))
            data[key] = value;
        else
            data.Add(key, value);

        return Ok();
    }

    [HttpPatch("patch")]
    public ActionResult Patch(string key, string value)
    {
        if (data.ContainsKey(key))
            data[key] = value;
        else
            return NotFound();

        return Ok();
    }

    [HttpDelete("delete")]
    public ActionResult Delete(string key)
    {
        if (data.ContainsKey(key))
            data.Remove(key);
        else
            return NotFound();

        return Ok();
    }
}