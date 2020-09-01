using GamesStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly System.Net.Http.HttpClient _client;

        public OrderDetailsService(System.Net.Http.HttpClient client)
        {
            _client = client;
        }
        public async Task<HttpResponseMessage> DeleteOrderDetails(int id)
        {
            string apiName = string.Format($"api/orderdetails/{id}");
            var result = await _client.DeleteAsync(apiName);
            return result;
        }

        public async Task<List<OrderDetail>> GetAllOrderDetails()
        {
            var apiName = "api/orderdetails";
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var products = System.Text.Json.JsonSerializer.Deserialize<List<OrderDetail>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return products;
        }

        public async Task<List<OrderDetail>> GetOrderDetails(int id)
        {
            var apiName = "api/orderdetails/" + id;
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var products = System.Text.Json.JsonSerializer.Deserialize<List<OrderDetail>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return products;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsByUser(string username)
        {
            var apiName = "api/orderdetails/GetOrderDetailByUser/" + username;
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var products = System.Text.Json.JsonSerializer.Deserialize<List<OrderDetail>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return products;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await JsonSerializer.DeserializeAsync<List<Product>>(
                await _client.GetStreamAsync($"api/products"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<HttpResponseMessage> InsertOrderDetails(string post)
        {
            string apiName = string.Format($"api/orderdetails");
            var response = string.Empty;
            var result = await _client.PostAsync(apiName, new StringContent(post, System.Text.Encoding.UTF8, "application/json"));

            return result;
        }

        public async Task<HttpResponseMessage> UpdateOrderDetails(string post, int id)
        {
            string apiName = string.Format($"api/orderdetails/{id}");
            var response = string.Empty;
            var result = await _client.PutAsync(apiName, new StringContent(post, System.Text.Encoding.UTF8, "application/json"));

            return result;
        }
    }
}
