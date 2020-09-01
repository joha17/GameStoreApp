using GamesStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public class UserAddressService : IUserAddressService
    {
        private readonly System.Net.Http.HttpClient _client;

        public UserAddressService(System.Net.Http.HttpClient client)
        {
            _client = client;
        }
        public Task<bool> DeleteUserAddress(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserAddress>> GetAllUserAddress()
        {
            var apiName = "api/useraddresses";
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var userAddresses = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<UserAddress>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return userAddresses;
        }

        public async Task<UserAddress> GetUserAddressDetails(string username)
        {
            var apiName = "api/useraddresses/" + username;
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var userAddress = System.Text.Json.JsonSerializer.Deserialize<UserAddress>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return userAddress;
        }

        public async Task<HttpResponseMessage> InsertUserAddress(string post)
        {
            string apiName = string.Format($"api/useraddresses");
            var response = string.Empty;
            var result = await _client.PostAsync(apiName, new StringContent(post, System.Text.Encoding.UTF8, "application/json"));

            return result;
        }

        public async Task<HttpResponseMessage> UpdateUserAddress(string post, string username)
        {
            string apiName = string.Format($"api/useraddresses/{username}");
            var response = string.Empty;
            var result = await _client.PutAsync(apiName, new StringContent(post, System.Text.Encoding.UTF8, "application/json"));

            return result;
        }
    }
}
