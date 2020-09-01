using GamesStoreWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GamesStoreWebApi.Repositories
{
    public class CategoryRepository
    {
        private readonly GameStoreContext _context;

        public CategoryRepository(GameStoreContext context)
        {
            _context = context;
        }

        public async Task<Category[]> Get()
        {
            return await _context.Categories.ToArrayAsync();
        }

        public async Task<Category> Get(int id)
        {
            var query = await _context.Categories.Where(p => p.IdCategory == id).FirstOrDefaultAsync();

            return query;
        }

        public async Task<Category> Save(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<bool> Update(Category request)
        {
            try
            {
                _context.Entry(request).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var model = await _context.Categories.FindAsync(id);
            if (model == null)
            {
                return false;
            }
            try
            {
                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

    }
}
