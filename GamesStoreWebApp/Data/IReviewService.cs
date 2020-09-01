using GamesStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public interface IReviewService
    {
        Task<List<Review>> GetAllReviews();
        Task<Review> GetReviewDetails(int id);
        Task<List<Review>> GetReviewByProduct(int productId);
        Task<HttpResponseMessage> InsertReview(string post);
        Task<bool> UpdateReview(string post, int id);
        Task<HttpResponseMessage> DeleteReview(int id);
    }
}
