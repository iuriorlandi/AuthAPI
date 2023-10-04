namespace AuthAPI.Services
{
    public class ServiceResult<T>
    {
        public bool Success { get; private set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        private const string SUCCESS_MESSAGE = "Operation performed successfully.";
        private const string FAILURE_MESSAGE = "Operation failed.";

        public static ServiceResult<T> CreateSucess(T data)
        {
            return new ServiceResult<T>
            {
                Message = SUCCESS_MESSAGE,
                Success = true,
                Data = data
            };
        }

        public static ServiceResult<T> CreateSucess()
        {
            return new ServiceResult<T>
            {
                Message = SUCCESS_MESSAGE,
                Success = true,
            };
        }

        public static ServiceResult<T> CreateFailure(T data, params string[] errorMessage)
        {
            return new ServiceResult<T>
            {
                Message = FAILURE_MESSAGE,
                Success = false,
                Data = data,
                Errors = errorMessage.ToList(),
            };
        }

        public static ServiceResult<T> CreateFailure(params string[] errorMessage)
        {
            return new ServiceResult<T>
            {
                Message = FAILURE_MESSAGE,
                Success = false,
                Errors = errorMessage.ToList()
            };
        }
    }
}
