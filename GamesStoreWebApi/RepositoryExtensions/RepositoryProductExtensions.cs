using GamesStoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApi.RepositoryExtensions
{
    public static class RepositoryProductExtensions
    {
        public static IQueryable<Products> Search(this IQueryable<Products> products, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
                return products;

            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();

            return products.Where(p => p.ProductName.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Products> FilterCategory(this IQueryable<Products> products, int filterTearm)
        {
            if (filterTearm == 0)
            {
                return products;
            }
            return products.Where(p => p.CategoryId.Equals(filterTearm));
        }
    }
}
