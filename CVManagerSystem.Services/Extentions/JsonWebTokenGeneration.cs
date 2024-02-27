using CVManagerSystem.Core.Dtos;
using CVManagerSystem.Data;
using CVManagerSystem.Data.DataContext.DbIdentity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CVManagerSystem.Services.Extentions
{
    public static class JsonWebTokenGeneration
    {
        public static string GenerateToken(IConfiguration _config, ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config?["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Country,user.Country)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public static async Task<bool> Authenticate(AppDataContext context, LoginDto userLogin)
        {
            var currentUser = context.Users.FirstOrDefault(x => x.Email.ToLower() == userLogin.Email.ToLower());
            if (currentUser != null)
            {
                return true;
            }
            return false;
        }
    }
}
