using Blazored.LocalStorage;
using GamesStoreWebApp.ApiAuth;
using GamesStoreWebApp.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<HttpResponseMessage> Register(RegisterModel registerModel)
        {
            registerModel.UserRole = "User";
            string apiName = string.Format($"api/account/register");
            var postData = JsonConvert.SerializeObject(registerModel);
            var result = await _httpClient.PostAsync(apiName, new StringContent(postData, System.Text.Encoding.UTF8, "application/json"));
            //var result2 = await _httpClient.PostAsJsonAsync<RegisterModel>("api/accounts", registerModel);

            return result;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            string apiName = string.Format($"api/login/authenticate");
            var loginAsJson = JsonConvert.SerializeObject(loginModel);
            var response = await _httpClient.PostAsync(apiName, new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = System.Text.Json.JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                loginResult.Error = "Usuario y contraseña no coinciden";
                return loginResult;
            }

            loginResult.Successful = true;
            await _localStorage.SetItemAsync("authToken", loginResult.Token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.UserName);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
