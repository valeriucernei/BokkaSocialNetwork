using System.Net;
using Common.Exceptions;
using Newtonsoft.Json;

namespace API.Infrastructure.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception has occured");

            await CreateExceptionResponseAsync(context, ex);
        }
    }

    private static Task CreateExceptionResponseAsync(HttpContext context, Exception ex)
    {
        if (ex is ApiException apiEx)
            context.Response.StatusCode = (int)apiEx.Code;
        else
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 
        
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = ex.Message
        }));
    }
}