using _02_ApiExample.Filters;
using Microsoft.AspNetCore.Mvc;

namespace _02_ApiExample.Controllers;

[ApiController]
[Route("[controller]")]
[LogActionFilter]
public class MathController : ControllerBase
{
    [HttpGet("Square")]
    public int Square(int x)
    {
        return x * x;
    }

    [HttpGet("Divide")]
    // public int Divide(int x, int y)
    // {
    //     return x / y;
    // }
    public ActionResult<int> Divide(int x, int y)
    {
        try
        {
            var z = x / y;
            return Ok(z);
        }
        catch (DivideByZeroException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet(template: "someCalc")]
    public async Task<ActionResult<int>> Calc()
    {
        var t1 = Task.Run<int>(() =>
        {
            Task.Delay(100).Wait();
            return 10;
        });
        
        var t2 = Task.Run<int>(() =>
        {
            Task.Delay(100).Wait();
            return 20;
        });
        var x1 = await t1;
        var x2 = await t2;

        return Ok(x1 + x2);
    }
}