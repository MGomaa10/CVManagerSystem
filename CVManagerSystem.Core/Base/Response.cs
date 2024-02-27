using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CVManagerSystem.Core.Base
{
    public class ResponseDto : IResponseDto
    {
        public ResponseDto()
        {
            IsSuccessed = false;
            Message = "";
            Errors = new List<string>();
        }
        public bool IsSuccessed { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public void Copy(ResponseDto x)
        {
            IsSuccessed = x.IsSuccessed;
            Message = x.Message;
            Data = x.Data;
            Errors = x.Errors;
        }
    }
}
