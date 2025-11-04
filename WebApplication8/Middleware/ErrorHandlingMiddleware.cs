using Microsoft.EntityFrameworkCore;
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
            _logger.LogError(ex, "Unhandled exception occurred");

            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                DbUpdateException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                InvalidOperationException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var response = new
            {
                statusCode,
                message = ex switch
                {
                    DbUpdateException => "A database error occurred while saving your changes.",
                    KeyNotFoundException => "The requested resource was not found.",
                    InvalidOperationException => ex.Message,
                    _ => "An unexpected error occurred. Please try again later."
                }
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
