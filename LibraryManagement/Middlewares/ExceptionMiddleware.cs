using System.Net;
using System.Text.Json;
using FluentValidation;
using LibraryManagement.Shared.DTOs.Common;

namespace LibraryManagement.API.Middlewares
{
    /// <summary>
    /// Middleware that handles exceptions occurring during the HTTP request pipeline execution.
    /// </summary>
    /// <remarks>
    /// This middleware intercepts unhandled exceptions occurring within the application, logs them,
    /// and generates a response to provide meaningful information to the client about the issue.
    /// Additionally, it differentiates behavior based on the application's environment (e.g., development).
    /// </remarks>
    /// <exception cref="Exception">
    /// Represents any unhandled error that occurs while processing an HTTP request.
    /// </exception>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware delegate in the request pipeline.</param>
        /// <param name="logger">The logger instance for recording exception details.</param>
        /// <param name="environment">The web hosting environment information provider.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the required parameters is null.</exception>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        /// Handles an incoming HTTP request and processes any exceptions that occur during the request pipeline execution.
        /// <param name="context">The HTTP context for the current request.</param>
        /// <returns>A task that represents the asynchronous operation for handling the middleware pipeline.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred. TraceId: {TraceId}", context.TraceIdentifier);
                await HandleExceptionAsync(context, ex, _environment.IsDevelopment());
            }
        }

        /// Processes exceptions that occur during the HTTP request pipeline and writes the appropriate response to the context.
        /// <param name="context">The HTTP context for the current request.</param>
        /// <param name="exception">The exception that occurred during request processing.</param>
        /// <param name="isDevelopment">Indicates whether the application is running in a development environment.</param>
        /// <returns>A task that represents the asynchronous operation of handling the exception and generating the response.</returns>
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, bool isDevelopment)
        {
            if (context.Response.HasStarted)
            {
                return;
            }

            context.Response.ContentType = "application/json";

            var response = new ApiResponse<object>
            {
                Success = false,
                Timestamp = DateTime.UtcNow,
                TraceId = context.TraceIdentifier
            };

            // Xử lý các loại exception cụ thể
            switch (exception)
            {
                case ValidationException validationEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Validation failed";
                    response.Errors = validationEx.Errors?.Select(e => e.ErrorMessage).ToList() ?? new List<string> { validationEx.Message };
                    break;

                case KeyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Resource not found";
                    response.Errors = new List<string> { exception.Message };
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = "Unauthorized access";
                    response.Errors = new List<string> { "You do not have permission to access this resource" };
                    break;

                case ArgumentException argumentEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Invalid argument";
                    response.Errors = new List<string> { argumentEx.Message };
                    break;

                case InvalidOperationException invalidOpEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Invalid operation";
                    response.Errors = new List<string> { invalidOpEx.Message };
                    break;

                case TimeoutException:
                    context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                    response.Message = "Request timeout";
                    response.Errors = new List<string> { "The request took too long to process" };
                    break;

                case NotSupportedException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                    response.Message = "Operation not supported";
                    response.Errors = new List<string> { exception.Message };
                    break;

                case BusinessLogicException businessEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Business logic error";
                    response.Errors = new List<string> { businessEx.Message };
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "An internal server error occurred";

                    // Chỉ hiển thị chi tiết lỗi trong development
                    if (isDevelopment)
                    {
                        response.Errors = new List<string>
                        {
                            exception.Message,
                            exception.StackTrace ?? "No stack trace available"
                        };
                        response.Data = new
                        {
                            Type = exception.GetType().Name,
                            Source = exception.Source,
                            InnerException = exception.InnerException?.Message
                        };
                    }
                    else
                    {
                        response.Errors = new List<string> { "An unexpected error occurred. Please contact support if the problem persists." };
                    }

                    break;
            }

            // Serialize với options phù hợp
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = isDevelopment,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            var jsonResponse = JsonSerializer.Serialize(response, jsonOptions);
            await context.Response.WriteAsync(jsonResponse);
        }
    }

    // Custom Exceptions
    /// <summary>
    /// Represents errors that occur during the execution of business logic within the application.
    /// </summary>
    /// <remarks>
    /// This exception is specifically designed to handle business logic errors that deviate from
    /// expected application operation. It can be used to provide meaningful messages to the user
    /// or application logs while maintaining clear separation of concerns for exception handling.
    /// </remarks>
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message) : base(message)
        {
        }

        public BusinessLogicException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}