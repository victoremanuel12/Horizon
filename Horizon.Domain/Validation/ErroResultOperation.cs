namespace Horizon.Domain.Validation
{
    public class ErroResultOperation
    {
        public class Result<T>
        {
            public bool Success { get; set; }
            public T Data { get; set; }
            public string ErrorMessage { get; set; }
            public int StatusCode { get; set; } 

            
            public void SetStatusCode(int statusCode)
            {
                StatusCode = statusCode;
            }
        }

    }
}
