using GamesStoreWebApi.Entities;
using GamesStoreWebApi.Models;
using GamesStoreWebApi.Paging;
using GamesStoreWebApi.RepositoryExtensions;
using GamesStoreWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApi.Repositories
{
    public class ProductRepository
    {
        private readonly GameStoreContext _context;

        public ProductRepository(GameStoreContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> Get([FromQuery] ProductParameters productParameters)
        {
            List<ProductViewModel> Listprod = await _context.Products.Search(productParameters.SearchTerm).FilterCategory(productParameters.FilterTerm).
                Select(p =>
                    new ProductViewModel
                    {
                        ProductName = p.ProductName,
                        IdProduct = p.IdProduct,
                        Description = p.Description,
                        UpdateDate = p.UpdateDate,
                        CreationDate = p.CreationDate,
                        CategoryId = p.CategoryId,
                        Discount = p.Discount,
                        Price = p.Price,
                        CategoryName = _context.Categories.FirstOrDefault(c => c.IdCategory == p.CategoryId).CategoryName.ToString(),
                        ImageUrl = p.ImageUrl
                    }
                ).ToListAsync();


            return Listprod;
        }

        public async Task<ProductViewModel> Get(int id)
        {
            var product = await _context.Products.Where(c => c.IdProduct == id).Select(p => new ProductViewModel
            {
                ProductName = p.ProductName,
                IdProduct = p.IdProduct,
                Description = p.Description,
                UpdateDate = p.UpdateDate,
                CreationDate = p.CreationDate,
                CategoryId = p.CategoryId,
                Discount = p.Discount,
                Price = p.Price,
                CategoryName = _context.Categories.FirstOrDefault(c => c.IdCategory == p.CategoryId).CategoryName.ToString(),
                ImageUrl = p.ImageUrl
            }).FirstOrDefaultAsync();

            return product;
        }

        public async Task<Products> Save(Products product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<bool> Update(Products request)
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
            var model = await _context.Products.FindAsync(id);
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
