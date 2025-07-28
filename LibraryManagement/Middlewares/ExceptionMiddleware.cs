using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Net;
using FluentValidation;
using LibraryManagement.Shared.DTOs.Common;

namespace LibraryManagement.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

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
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message) : base(message) { }
        public BusinessLogicException(string message, Exception innerException) : base(message, innerException) { }
    }

}