using GamesStoreWebApp.Entities;
using GamesStoreWebApp.Features;
using GamesStoreWebApp.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace GamesStoreWebApp.Data
{
    public class ProductService : IProductService
    {
        private readonly System.Net.Http.HttpClient _client;
        public ILocalStorageService _localStorageService { get; }

        public ProductService(System.Net.Http.HttpClient client, ILocalStorageService localStorageService)
        {
            _client = client;
            _localStorageService = localStorageService;
        }

        public async Task<PagingResponse<Product>> GetProducts(ProductParameters productParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString(),
                ["searchTerm"] = productParameters.SearchTerm == null ? "" : productParameters.SearchTerm
            };

            var apiName = "api/products";
            var requestmessage = new HttpRequestMessage(HttpMethod.Get,apiName);
            var responseAuth = await _client.SendAsync(requestmessage);
            

            var responseStatusCode = responseAuth.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var response = await _client.GetAsync(QueryHelpers.AddQueryString(apiName, queryStringParam));
                var content = await response.Content.ReadAsStringAsync();

                var pagingResponse = new PagingResponse<Product>
                {
                    Items = JsonSerializer.Deserialize<List<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                    MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                };

                return pagingResponse;
            }
            else
            {
                return null;
            }
            //if (!response.IsSuccessStatusCode)
            //{
            //    throw new ApplicationException(content);
            //}

            
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var apiName = "api/products";
            
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var products = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            return products;
        }

        public async Task<Product> GetProductDetails(int id)
        {
            var apiName = "api/products/" + id;
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var products = System.Text.Json.JsonSerializer.Deserialize<Product>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
           
            return products;
        }


        public async Task<HttpResponseMessage> InsertProduct(string post)
        {
            string apiName = string.Format($"api/products");
            var response = string.Empty;
            var result = await _client.PostAsync(apiName, new StringContent(post, System.Text.Encoding.UTF8, "application/json"));

            return result;
        }

        public async Task<HttpResponseMessage> UpdateProduct(string post, int id)
        {
            string apiName = string.Format($"api/products/{id}");
            var response = string.Empty;
            var result = await _client.PutAsync(apiName, new StringContent(post, System.Text.Encoding.UTF8, "application/json"));
            
            return result;
        }

        public async Task<HttpResponseMessage> DeleteProduct(int id)
        {
            string apiName = string.Format($"api/products/{id}");
            var response = string.Empty;
            var result = await _client.DeleteAsync(apiName);
            //HttpResponseMessage result = await client.PutAsJsonAsync($"{apiName}/{category.IdCategory}", category);
            if (result.IsSuccessStatusCode)
            {
                response = result.StatusCode.ToString();
            }
            else
            {
                response = result.StatusCode.ToString();
            }

            return result;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await JsonSerializer.DeserializeAsync<List<Category>>(
                await _client.GetStreamAsync($"api/categories"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
