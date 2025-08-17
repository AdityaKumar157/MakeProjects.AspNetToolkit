using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static MakeProjects.AspNetToolkit.Abstractions.Exceptions.Exceptions;

namespace MakeProjects.AspNetToolkit.Infrastructure.Middlewares
{
    /// <summary>
    /// A base middleware for handling global exceptions and request logging.
    /// </summary>
    public class BaseRequestHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BaseRequestHandlingMiddleware> _logger;

        /// <summary>
        /// Constructs a new instance of the <see cref="BaseRequestHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public BaseRequestHandlingMiddleware(RequestDelegate next, ILogger<BaseRequestHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Handles incoming HTTP requests, logs request/response information,
        /// and catches unhandled exceptions to return a consistent error response.
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var method = context.Request.Method;
                var path = context.Request.Path.Value;
                _logger.LogInformation(FormattableString.Invariant($"Handling request: {method} {path}"));
                await _next(context);
                _logger.LogInformation(FormattableString.Invariant($"Request handled successfully: {method} {path}"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred while processing the request.");
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            HttpStatusCode statusCode;
            string message = exception.Message;

            switch (exception)
            {
                case ArgumentNullException:
                case BadRequestException:
                    {
                        statusCode = HttpStatusCode.BadRequest;
                        break;
                    }
                case KeyNotFoundException:
                case NotFoundException:
                    {
                        statusCode = HttpStatusCode.NotFound;
                        break;
                    }
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case ConflictException:
                    statusCode = HttpStatusCode.Conflict;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred.";
                    break;
            }

            var response = new
            {
                status = (int)statusCode,
                error = message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            logger.LogError(exception, FormattableString.Invariant($"Exception handled: StatusCode:{statusCode} - Message:{message}"));
            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
    }

    /// <summary>
    /// Extension method for easily adding BaseRequestHandlingMiddleware.
    /// </summary>
    public static class BaseRequestHandlingMiddlewareExtensions
    {
        /// <summary>
        /// Adds BaseRequestHandlingMiddleware to the specified <see cref="IApplicationBuilder"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseBaseRequestHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BaseRequestHandlingMiddleware>();
        }
    }
}
