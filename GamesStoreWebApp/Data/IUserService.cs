using GamesStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfo>> GetAllUsers();
        Task<UserInfo> GetUsersDetails(string username);
        Task<HttpResponseMessage> InsertUser(string post);
        Task<HttpResponseMessage> UpdateUser(string post, string id);
        Task<bool> DeleteUserAddress(string id);
    }
}
