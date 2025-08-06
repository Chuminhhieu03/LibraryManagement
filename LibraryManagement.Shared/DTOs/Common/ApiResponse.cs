namespace LibraryManagement.Shared.DTOs.Common
{
    /// <summary>
    /// Represents a standardized response structure for API endpoints.
    /// </summary>
    /// <typeparam name="T">The type of data returned in the response.</typeparam>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? TraceId { get; set; }

        /// <summary>
        /// Creates a success response result with an optional message.
        /// </summary>
        /// <param name="message">An optional message to describe the success response. Defaults to "Success".</param>
        /// <returns>An instance of <see cref="ApiResponse{T}"/> with success set to true and the provided message.</returns>
        public static ApiResponse<T> SuccessResult(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
            };
        }

        /// <summary>
        /// Creates a success response result with an optional message.
        /// </summary>
        /// <param name="message">An optional message to describe the success response. Defaults to "Success".</param>
        /// <returns>An instance of <see cref="ApiResponse{T}"/> with success set to true and the provided message.</returns>
        public static ApiResponse<T> SuccessResult(string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
            };
        }

        /// <summary>
        /// Creates an error response result with a single error message and an optional status code.
        /// </summary>
        /// <param name="error">A single error message to include in the response.</param>
        /// <param name="statusCode">An optional status code representing the error. Defaults to 400.</param>
        /// <returns>An instance of <see cref="ApiResponse{T}"/> with success set to false and the provided error details.</returns>
        public static ApiResponse<T> ErrorResult(string error, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Errors = new List<string> { error },
                Message = "Error occurred"
            };
        }

        /// <summary>
        /// Creates an error response result with the specified details.
        /// </summary>
        /// <param name="errors">A list of error messages to include in the response.</param>
        /// <param name="message">An optional message describing the error. Defaults to "Error occurred".</param>
        /// <param name="statusCode">An optional status code representing the error. Defaults to 400.</param>
        /// <returns>An instance of <see cref="ApiResponse{T}"/> with success set to false and the provided error details.</returns>
        public static ApiResponse<T> ErrorResult(List<string> errors, string message = "Error occurred", int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors,
            };
        }
    }
}