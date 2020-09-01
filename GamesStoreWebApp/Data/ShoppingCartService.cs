using GamesStoreWebApp.ApiAuth;
using GamesStoreWebApp.Entities;
using GamesStoreWebApp.Features;
using GamesStoreWebApp.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly System.Net.Http.HttpClient _client;

        public ShoppingCartService(System.Net.Http.HttpClient client)
        {
            _client = client;
        }

        public async Task<PagingResponse<ShoppingCart>> GetProductsCatalog(ProductParameters productParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString(),
                ["searchTerm"] = productParameters.SearchTerm == null ? "" : productParameters.SearchTerm,
                ["filterTerm"] = productParameters.FilterTerm.ToString() == "0" ? "0" : productParameters.FilterTerm.ToString()
            };

            var apiName = "api/products/GetProductsCatalog";
            var requestmessage = new HttpRequestMessage(HttpMethod.Get, apiName);
            var responseAuth = await _client.SendAsync(requestmessage);

            var responseStatusCode = responseAuth.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var response = await _client.GetAsync(QueryHelpers.AddQueryString(apiName, queryStringParam));
                var content = await response.Content.ReadAsStringAsync();

                var pagingResponse = new PagingResponse<ShoppingCart>
                {
                    Items = JsonSerializer.Deserialize<List<ShoppingCart>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                    MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                };

                return pagingResponse;
            }
            else
            {
                return null;
            }

            //var response = await _client.GetAsync(QueryHelpers.AddQueryString(apiName, queryStringParam));
            //var content = await response.Content.ReadAsStringAsync();
            //if (!response.IsSuccessStatusCode)
            //{
            //    throw new ApplicationException(content);
            //}

            //var pagingResponse = new PagingResponse<ShoppingCart>
            //{
            //    Items = JsonSerializer.Deserialize<List<ShoppingCart>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
            //    MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            //};

            //return pagingResponse;
        }

        public async Task<ShoppingCart> GetProductCatalogDetails(int id)
        {
            var apiName = "api/products/" + id;
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();
            var products = System.Text.Json.JsonSerializer.Deserialize<ShoppingCart>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return products;
        }


        public async Task<IEnumerable<Models.ShoppingCart>> GetAllCatalogProducts()
        {
            var apiName = "api/products";
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var products = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Models.ShoppingCart>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return products;
        }
    }
}
