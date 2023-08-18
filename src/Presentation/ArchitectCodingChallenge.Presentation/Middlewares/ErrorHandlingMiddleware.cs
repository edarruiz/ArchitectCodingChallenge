using System.Net;
using Serilog;

namespace ArchitectCodingChallenge.Presentation.Middlewares;

/// <summary>
/// Represents the error handling middleware;
/// </summary>
public class ErrorHandlingMiddleware {
    #region Fields
    private readonly RequestDelegate next;
    #endregion

    #region Ctor
    /// <summary>
    /// Initializes a new instance of the class <see cref="ErrorHandlingMiddleware"/>.
    /// </summary>
    /// <param name="next">The request delegate</param>
    public ErrorHandlingMiddleware(RequestDelegate next) {
        this.next = next;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Represents the middleware invocation context.
    /// </summary>
    /// <param name="context">The http context of the request.</param>
    /// <returns></returns>
    public async Task Invoke(HttpContext context) {
        try {
            await next(context);
        } catch (Exception ex) {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles the exception serialization.
    /// </summary>
    /// <param name="context">The http context of the request.</param>
    /// <param name="exception">The exception of the context.</param>
    /// <returns></returns>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception) {
        Log.Error(exception, "Error");

        var code = HttpStatusCode.InternalServerError;

        var result = System.Text.Json.JsonSerializer.Serialize(new { error = exception?.Message });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
    #endregion
}
