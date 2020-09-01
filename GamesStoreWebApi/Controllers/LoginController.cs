using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GamesStoreWebApi.Models;
using GamesStoreWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GamesStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly GameStoreContext _context;
        private readonly IConfiguration _config;

        public LoginController(GameStoreContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel login)
        {
            IActionResult response = Unauthorized();
            User user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            else
            {
                return BadRequest(new { message = "Username and Password incorrect" });
            }
            return response;

        }

        User AuthenticateUser(AuthenticationModel loginCredentials)
        {
            User user = _context.Users.SingleOrDefault(x => x.UserName == loginCredentials.UserName && x.Password == loginCredentials.Password);
            return user;
        }

        string GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("fullName", userInfo.FirstName.ToString() + "_" + userInfo.FirstName.ToString()),
                new Claim("role",userInfo.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
