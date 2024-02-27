using Azure;
using CVManagerSystem.Core.Base;
using CVManagerSystem.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManagerSystem.Services.Interfaces
{
    public interface IIdentityServices
    {
        Task<IActionResult> Login(LoginDto login);
        Task<IResponseDto> Register(RegisterDto model);
    }
}
