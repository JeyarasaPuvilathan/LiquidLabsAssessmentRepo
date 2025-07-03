using System.Net;
using System.Text.Json;
using LiquidLabsApp.Exceptions;

namespace LiquidLabsApp.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Continue pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";

                switch (ex)
                {
                    case NotFoundException notFound:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ApplicationException appEx:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var problem = new
                {
                    status = context.Response.StatusCode,
                    title = ex.Message,
                    traceId = context.TraceIdentifier
                };

                var json = JsonSerializer.Serialize(problem);
                await context.Response.WriteAsync(json);
            }
        }
    }
}