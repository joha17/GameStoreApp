using GamesStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public class UserService : IUserService
    {
        private readonly System.Net.Http.HttpClient _client;

        public UserService(System.Net.Http.HttpClient client)
        {
            _client = client;
        }
        public Task<bool> DeleteUserAddress(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserInfo>> GetAllUsers()
        {
            var apiName = "api/user";
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var userAddresses = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<UserInfo>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return userAddresses;
        }

        public async Task<UserInfo> GetUsersDetails(string username)
        {
            var apiName = "api/user/" + username;
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var userAddress = System.Text.Json.JsonSerializer.Deserialize<UserInfo>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return userAddress;
        }

        public async Task<HttpResponseMessage> InsertUser(string post)
        {
            string apiName = string.Format($"api/user");
            var response = string.Empty;
            var result = await _client.PostAsync(apiName, new StringContent(post, System.Text.Encoding.UTF8, "application/json"));

            return result;
        }

        public async Task<HttpResponseMessage> UpdateUser(string post, string username)
        {
            string apiName = string.Format($"api/user/{username}");
            var response = string.Empty;
            var result = await _client.PutAsync(apiName, new StringContent(post, System.Text.Encoding.UTF8, "application/json"));

            return result;
        }
    }
}
