using System.Net;
using ILogger = Serilog.ILogger;

namespace BioTekno.Task.Middleware;
using System.Threading.Tasks;

public class GlobalErrorHandlingMiddleware 
{
    private readonly RequestDelegate _next;
    static readonly ILogger Log = Serilog.Log.ForContext<GlobalErrorHandlingMiddleware>();
    public GlobalErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var now = DateTime.UtcNow;
        Log.Error($"{now.ToString("HH:mm:ss")} : {ex}");
        return httpContext.Response.WriteAsync(new ErrorResultModel()
        {
            StatusCode = httpContext.Response.StatusCode,
            Message = ex.Message
        }.ToString());
    }
    
}