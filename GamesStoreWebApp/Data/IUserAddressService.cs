using GamesStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public interface IUserAddressService
    {
        Task<IEnumerable<UserAddress>> GetAllUserAddress();
        Task<UserAddress> GetUserAddressDetails(string username);
        Task<HttpResponseMessage> InsertUserAddress(string post);
        Task<HttpResponseMessage> UpdateUserAddress(string post, string id);
        Task<bool> DeleteUserAddress(string id);
    }
}
