using GamesStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public interface IAuthenticationService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<HttpResponseMessage> Register(RegisterModel registerModel);
    }
}
