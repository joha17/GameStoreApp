using GamesStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public class ReviewService : IReviewService
    {
        private readonly System.Net.Http.HttpClient _client;
        public ReviewService(System.Net.Http.HttpClient client)
        {
            _client = client;
        }
        public async Task<HttpResponseMessage> DeleteReview(int id)
        {
            string apiName = string.Format($"api/reviews/{id}");
            var result = await _client.DeleteAsync(apiName);
            
            return result;
        }

        public async Task<List<Review>> GetAllReviews()
        {
            var apiName = "api/reviews";
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var reviews = System.Text.Json.JsonSerializer.Deserialize<List<Review>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return reviews;
        }

        public async Task<List<Review>> GetReviewByProduct(int productId)
        {
            var apiName = "api/reviews/GetReviewsByProduct/" + productId;
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var reviews = System.Text.Json.JsonSerializer.Deserialize<List<Review>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return reviews;
        }

        public async Task<Review> GetReviewDetails(int id)
        {
            var apiName = "api/reviews/" + id;
            var response = await _client.GetAsync(apiName);
            var content = await response.Content.ReadAsStringAsync();

            var reviews = System.Text.Json.JsonSerializer.Deserialize<Review>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return reviews;
        }

        public async Task<HttpResponseMessage> InsertReview(string post)
        {
            string apiName = string.Format($"api/reviews");
            var result = await _client.PostAsync(apiName, new StringContent(post, System.Text.Encoding.UTF8, "application/json"));

            return result;
        }

        public async Task<bool> UpdateReview(string post, int id)
        {
            string apiName = string.Format($"api/reviews/{id}");
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
