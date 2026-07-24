using System.Net;
using System.Text.Json;

namespace Base.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var realException = GetRealException(ex);

            _logger.LogError(realException, realException.Message);

            await HandleExceptionAsync(context, realException);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorLocation = GetErrorLocation(exception);

        var response = new
        {
            message = exception.Message,
            statusCode = context.Response.StatusCode,
            file = errorLocation.file,
            line = errorLocation.line,
            traceId = context.TraceIdentifier
        };

        var json = JsonSerializer.Serialize(response,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        await context.Response.WriteAsync(json);
    }

    private static Exception GetRealException(Exception ex)
    {
        if (ex is AggregateException agg && agg.InnerException != null)
            return agg.InnerException;

        return ex.InnerException ?? ex;
    }

    private static (string? file, int? line) GetErrorLocation(Exception exception)
    {
        if (exception.StackTrace == null)
            return (null, null);

        var lines = exception.StackTrace.Split('\n');

        foreach (var line in lines)
        {
            if (line.Contains(".cs:line"))
            {
                var parts = line.Trim().Split(" in ");

                if (parts.Length > 1)
                {
                    var filePart = parts[1];
                    var fileSplit = filePart.Split(":line");

                    return (fileSplit[0], int.Parse(fileSplit[1]));
                }
            }
        }

        return (null, null);
    }
}