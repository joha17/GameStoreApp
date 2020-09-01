using GamesStoreWebApi.Helpers;
using GamesStoreWebApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GamesStoreWebApi.Services
{
    public class UserInfo : IUser
    {
        private readonly GameStoreContext _context;
        private readonly AppSettings _appSettings;

        public UserInfo(GameStoreContext context,IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        public User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);

            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }
    }
}
