using Microsoft.AspNetCore.Mvc.Filters;

namespace _02_ApiExample.Filters;

public class LogActionFilter : ActionFilterAttribute
{
    // public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    // {
    //     context.Response.StatusCode = 200;
    //     using (var writer = new StreamWriter(context.Response.Body))
    //     {
    //         await writer.WriteLineAsync("Done!");
    //     }
    // }

    public override async void OnActionExecuting(ActionExecutingContext context)
    {
        var controllerName = context.RouteData.Values["controller"];
        var actionName = context.RouteData.Values["action"];
        var message = $"Executing action {actionName} on controller {controllerName}.\n";

        File.AppendAllText("log.txt", message);

        base.OnActionExecuting(context);
    }

    public override async void OnActionExecuted(ActionExecutedContext context)
    {
        var controllerName = context.RouteData.Values["controller"];
        var actionName = context.RouteData.Values["action"];
        var message = $"Executing action {actionName} on controller {controllerName}.\n";
        
        File.AppendAllText("log.txt", message);

        base.OnActionExecuted(context);
    }
}