using CVManagerSystem.Core.Base;
using CVManagerSystem.Core.Dtos;
using CVManagerSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVManagerSystem.API.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IIdentityServices _accountService;
        public AccountController(
           IIdentityServices accountService,
           IResponseDto response,
           IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            return await _accountService.Login(login);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IResponseDto> Register(RegisterDto registerDto)
        {
            _response = await _accountService.Register(registerDto);
            return _response;
        }
    }
}
