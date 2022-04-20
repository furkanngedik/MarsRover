namespace MarsRover.Core.Models
{
    public class ResponseModel<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public ResponseModel(bool success, T data)
        {
            this.Success = success;
            this.Data = data;
        }
        public ResponseModel(string message, bool success, T data)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
        }

    }
}
