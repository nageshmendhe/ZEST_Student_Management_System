using System.Net;
using System.Text.Json;

namespace ZEST_Student_Management_System.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// The logger
        /// </summary>
        public readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="logger">The logger.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An unhandled exception occurred while processing the request.");

                await HandleExceptionAsync(context);
            }
        }

        /// <summary>
        /// Handles the exception asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        private static async Task HandleExceptionAsync(
            HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = "An unexpected error occurred. Please try again later."
            };

            var jsonResponse = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(jsonResponse);
        }




    }
    }
