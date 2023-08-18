using Serilog.Context;

namespace ArchitectCodingChallenge.Presentation.Middlewares;

/// <summary>
/// Represents the middleware for logging.
/// </summary>
public class RequestSerilogMiddleware {
    #region Fields
    private readonly RequestDelegate _next;
    #endregion

    #region Ctor
    /// <summary>
    /// Initializes a new instance of the class <see cref="RequestSerilogMiddleware"/>.
    /// </summary>
    /// <param name="next"></param>
    public RequestSerilogMiddleware(RequestDelegate next) {
        _next = next;
    }
    #endregion


    #region Methods
    /// <summary>
    /// Represents the middleware invocation context.
    /// </summary>
    /// <param name="context">The http context of the request.</param>
    /// <returns></returns>
    public Task Invoke(HttpContext context) {
        using (LogContext.PushProperty("UserName", context?.User?.Identity?.Name ?? "anônimo")) {
            return _next.Invoke(context!);
        }
    }
    #endregion
}
