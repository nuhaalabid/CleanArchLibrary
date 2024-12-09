using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public  class OperationResult<T>
    {
        public bool IsSuccessful { get; private set; } 
        public string Message { get; private set; }
        public T Data { get; private set; }
        public string ErrorMessage { get; private set; }

        private OperationResult(bool isSuccessful, string message, T data, string errorMessage)
        {
            IsSuccessful = isSuccessful;  
            Message = message;
            ErrorMessage = errorMessage;
            Data = data;
        }

        public static OperationResult<T> Success(T data, string message = "Operation successful")  
        {
            return new OperationResult<T>(true, message, data, null);
        }

        public static OperationResult<T> Failure(string errorMessage, string message = "Operation failed")
        {
            return new OperationResult<T>(false, message, default(T), errorMessage);
        }
    }
}



