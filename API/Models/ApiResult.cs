namespace API.Models
{
    public class ApiResult<T> where T : class
    {
        public string errorMessage = "";
        public bool isError;
        public int errorCode;
        public T value;

        public static ApiResult<T> Error(int errorCode, string errorMessage, T value = null)
        {
            return new ApiResult<T>
            {
                isError = true,
                errorCode = errorCode,
                errorMessage = errorMessage,
                value = value 
            };
        }
        public static ApiResult<T> OK(T value, bool errorMessage = false)
        {
            return new ApiResult<T>
            {
                isError = errorMessage,
                value = value
            };
        }
    }
}