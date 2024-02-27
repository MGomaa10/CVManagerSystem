using Azure;
using CVManagerSystem.Core.Base;
using CVManagerSystem.Core.Dtos;
using CVManagerSystem.Data;
using CVManagerSystem.Data.DataContext.DbIdentity;
using CVManagerSystem.Services.Extentions;
using CVManagerSystem.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManagerSystem.Services.Implementations
{
    public class IdentityService : IIdentityServices
    {
        private readonly AppDataContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IResponseDto _response;
        public IdentityService(AppDataContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IResponseDto response)
        {
            _context = context;
            _configuration = configuration;
            _response = response;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Login(LoginDto login)
        {
            var result = Microsoft.AspNetCore.Identity.SignInResult.Failed;
            var token = "";
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);

            var checkUser = JsonWebTokenGeneration.Authenticate(_context, login);
            if (checkUser is not null)
            {
                result = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);
            }

            if (!result.Succeeded) { 
            return new BadRequestObjectResult(new { StatusCode = 404, Value = "not Found" });
            }
            else
            {
                 token = JsonWebTokenGeneration.GenerateToken(_configuration, user);
            }

            var UserReturn = user.Adapt<ReadUser>();
            var User = new
            {
                Token = token,
                User = UserReturn
            };

            return new OkObjectResult(User);
        }

        public async Task<IResponseDto> Register(RegisterDto request)
        {
            var emailFound = await _userManager.FindByEmailAsync(request.Email.Trim().ToLower());
            if (emailFound != null)
            {
                _response.IsSuccessed = false;
                _response.Errors.Add("This email is Exist");
                return _response;
            }

            var applicationUser = new ApplicationUser()
            {
                EmailConfirmed = true,
                UserName = request.Email,
                Email = request.Email,
                LockoutEnabled = false,
                Address = request.Address,
                Country = request.Country,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            var result = await _userManager.CreateAsync(applicationUser, request.Password);

            if (result.Succeeded)
            {
                _response.IsSuccessed = true;
                return _response;
            }
            _response.IsSuccessed = false;
            return _response;
        }
    }
}
