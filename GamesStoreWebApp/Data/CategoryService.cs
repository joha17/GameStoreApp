using GamesStoreWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly System.Net.Http.HttpClient _client;

        public CategoryService(System.Net.Http.HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> DeleteCategory(int id)
        {
            string apiName = string.Format($"api/category/{id}");
            var result = await _client.DeleteAsync(apiName);
            
            return result;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var apiName = "api/categories";
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var products = System.Text.Json.JsonSerializer.Deserialize<List<Category>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return products;
        }

        public async Task<List<Category>> GetAllCategoriesCatalog()
        {
            var apiName = "api/categories/GetCategoriesCatalog";
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var products = System.Text.Json.JsonSerializer.Deserialize<List<Category>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return products;
        }

        public async Task<Category> GetCategoryDetails(int id)
        {
            var apiName = "api/categories/" + id;
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var products = System.Text.Json.JsonSerializer.Deserialize<Category>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return products;
        }

        public async Task<HttpResponseMessage> InsertCategory(string post)
        {
            string apiName = string.Format($"api/categories");
            var response = string.Empty;
            var result = await _client.PostAsync(apiName, new StringContent(post, System.Text.Encoding.UTF8, "application/json"));
            
            return result;
        }

        public async Task<bool> UpdateCategory(string post, int id)
        {
            string apiName = string.Format($"api/categories/{id}");
            var response = string.Empty;
            var result = await _client.PutAsync(apiName, new StringContent(post, System.Text.Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
