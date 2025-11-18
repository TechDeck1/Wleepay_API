using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Response
{
        public class GenericResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }

            public GenericResponse(bool success, string message)
            {
                Success = success;
                Message = message;
            }
        }

        public class GenericResponse<T> : GenericResponse
        {
            public T Data { get; set; }

            public GenericResponse(bool success, string message, T data = default)
                : base(success, message)
            {
                Data = data;
            }
        }
}
