using GamesStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Data
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<List<Category>> GetAllCategoriesCatalog();
        Task<Category> GetCategoryDetails(int id);
        Task<HttpResponseMessage> InsertCategory(string post);
        Task<bool> UpdateCategory(string post, int id);
        Task<HttpResponseMessage> DeleteCategory(int id);
    }
}
