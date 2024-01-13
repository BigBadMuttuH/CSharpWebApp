using System.Diagnostics;
using _01_WebAppLecture.Models;
using Microsoft.AspNetCore.Mvc;

namespace _01_WebAppLecture.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewData["Text"] = "Hello World!";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // http://localhost:5156/Home/HelloWorld
    public IActionResult HelloWorld()
    {
        return Content("Hello World");
    }

    // http://localhost:5156/Home/Exception
    public IActionResult Exception()
    {
        throw new Exception("I am Execution");
        // Query
        // http://localhost:5156/Home/Exception?a=2&b=5
        return Content("Hello World");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}