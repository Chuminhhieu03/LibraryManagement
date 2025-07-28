namespace LibraryManagement.Shared.DTOs.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? TraceId { get; set; }

        public static ApiResponse<T> SuccessResult(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
            };
        }

        public static ApiResponse<T> SuccessResult(string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
            };
        }

        public static ApiResponse<T> ErrorResult(string error, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Errors = new List<string> { error },
                Message = "Error occurred"
            };
        }

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