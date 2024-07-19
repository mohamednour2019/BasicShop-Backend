using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.Domain.Entities
{
    public class ResponseModel<TResponseDto> where TResponseDto : class
    {
        public ResponseModel(TResponseDto responseDto,string message,bool success) {
            Data = responseDto;
            Message = message;
            Success= success;  
        }
        public bool Success { get;}
        public string Message { get;}
        public TResponseDto? Data { get;}
    }
}
