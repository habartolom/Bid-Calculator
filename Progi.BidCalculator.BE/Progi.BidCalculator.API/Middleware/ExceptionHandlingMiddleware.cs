using System.Net;
using System.Text.Json;
using FluentValidation;

namespace Progi.BidCalculator.API.Middleware;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger
)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

        context.Response.ContentType = "application/json";

        var (statusCode, message, errors) = exception switch
        {
            ValidationException validationException => (
                HttpStatusCode.BadRequest,
                "Validation errors",
                validationException.Errors.Select(e => new
                {
                    property = e.PropertyName,
                    error = e.ErrorMessage
                }).ToList<object>()
            ),
            ArgumentException argumentException => (
                HttpStatusCode.BadRequest,
                argumentException.Message,
                new List<object>()
            ),
            _ => (
                HttpStatusCode.InternalServerError,
                "An internal error has occurred. Please try again later.",
                new List<object>()
            )
        };

        context.Response.StatusCode = (int)statusCode;

        var problemDetails = new
        {
            type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            title = message,
            status = (int)statusCode,
            detail = exception.Message,
            errors = errors.Count != 0 ? errors : null,
            traceId = context.TraceIdentifier
        };

        var json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}

