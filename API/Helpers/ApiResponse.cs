namespace API.Helpers
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public T Data { get; }

        public ApiResponse(bool isSuccess, string message, T data = default)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> Success(T data) => new ApiResponse<T>(true, "Success Responsess", data);
        public static ApiResponse<T> Failure(string message) => new ApiResponse<T>(false, message);
    }
}
