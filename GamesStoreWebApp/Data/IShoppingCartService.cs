using GamesStoreWebApp.Features;
using GamesStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public interface IShoppingCartService
    {
        //Paging
        Task<PagingResponse<ShoppingCart>> GetProductsCatalog(ProductParameters productParameters);


        Task<Models.ShoppingCart> GetProductCatalogDetails(int id);
        Task<IEnumerable<Models.ShoppingCart>> GetAllCatalogProducts();
    }
}
