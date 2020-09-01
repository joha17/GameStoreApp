using GamesStoreWebApi.Entities;
using GamesStoreWebApi.Models;
using GamesStoreWebApi.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApi.Data
{
    interface IProductRepository
    {
        Task<PagedList<Products>> GetProducts(ProductParameters productParameters);
    }
}
