using GamesStoreWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApi.Repositories
{
    public class ReviewRepository
    {
        private readonly GameStoreContext _context;

        public ReviewRepository(GameStoreContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> Get()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> Get(int id)
        {
            var query = await _context.Reviews.Where(p => p.Id == id).FirstOrDefaultAsync();

            return query;
        }

        public async Task<List<Review>> GetByProduct(int productId)
        {
            var query = await _context.Reviews.Where(p => p.ProductId == productId).ToListAsync();

            return query;
        }

        public async Task<Review> Save(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> Update(Review request)
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
            var model = await _context.Reviews.FindAsync(id);
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
