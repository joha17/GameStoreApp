using GamesStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public interface IOrderDetailsService
    {
        Task<List<OrderDetail>> GetAllOrderDetails();
        Task<List<OrderDetail>> GetOrderDetails(int id);
        Task<List<OrderDetail>> GetOrderDetailsByUser(string username);
        Task<HttpResponseMessage> InsertOrderDetails(string post);
        Task<HttpResponseMessage> UpdateOrderDetails(string post, int id);
        Task<HttpResponseMessage> DeleteOrderDetails(int id);
        Task<List<Product>> GetProducts();
    }
}
