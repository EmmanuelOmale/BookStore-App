using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDomain.Entities.Dtos
{
    public class ResponseDto<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T Data { get; set; }


        public static ResponseDto<T> Fail(IEnumerable<string> errors, int statusCode = (int)HttpStatusCode.BadRequest)
        {
            return new ResponseDto<T>
            {
                StatusCode = statusCode,
                IsSuccess = false,
                Errors = errors
            };
           
        }

        public static ResponseDto<T> Success(T data, string successMessage = "",  int statusCode = (int)HttpStatusCode.OK)
        {
            return new ResponseDto<T>
            {
                StatusCode = statusCode,
                Message = successMessage,
                Data = data,
            };
        }
    }
}
