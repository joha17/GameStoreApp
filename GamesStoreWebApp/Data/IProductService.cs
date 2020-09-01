using GamesStoreWebApp.Features;
using GamesStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public interface IProductService
    {
        Task<PagingResponse<Product>> GetProducts(ProductParameters productParameters);

        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductDetails(int id);
        Task<HttpResponseMessage> InsertProduct(string post);
        Task<HttpResponseMessage> UpdateProduct(string post, int id);
        Task<HttpResponseMessage> DeleteProduct(int id);
        Task<List<Category>> GetCategories();
        
    }
}
