using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManagerSystem.Core.Base
{
    public interface IResponseDto
    {
        bool IsSuccessed { get; set; }
        string Message { get; set; }
        List<string> Errors { get; set; }
    }
}
