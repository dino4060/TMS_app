using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace back_end_for_TMS.Infrastructure.Response;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, ">>> JWT VALIDATION FAILED <<<");

        var (statusCode, title) = exception switch
        {
            SecurityTokenException => (StatusCodes.Status401Unauthorized, "Unauthenticated"),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthenticated"),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource Not Found"),
            InvalidOperationException => (StatusCodes.Status500InternalServerError, "Internal Server Error"),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };

        var problemDetails = new ProblemDetails();
        problemDetails.Extensions.Add("instance", httpContext.Request.Path.Value);
        problemDetails.Extensions.Add("success", false);
        problemDetails.Extensions.Add("status", statusCode);
        problemDetails.Extensions.Add("title", title);
        problemDetails.Extensions.Add("message", exception.Message);

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}