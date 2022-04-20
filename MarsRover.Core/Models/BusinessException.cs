using System;

namespace MarsRover.Core.Models
{
    public class BusinessException : Exception
    {
        public int ErrorCode { get; set; }
        public BusinessException(string message) : base(message)
        {

        }
        public BusinessException(int errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }
    }
}
