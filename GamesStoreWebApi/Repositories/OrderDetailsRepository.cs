using GamesStoreWebApi.Models;
using GamesStoreWebApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApi.Repositories
{
    public class OrderDetailsRepository
    {
        private readonly GameStoreContext _context;

        public OrderDetailsRepository(GameStoreContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDetail>> Get()
        {

            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<List<OrderDetailsViewModel>> GetByOrderId(int orderid)
        {
            var query = await _context.OrderDetails
                .Where(x => x.OrderId == orderid)
                .Select(p =>
                    new OrderDetailsViewModel
                    {
                        ProductName = _context.Products.FirstOrDefault(c => c.IdProduct == p.IdProduct).ProductName.ToString(),
                        IdProduct = p.IdProduct,
                        Subtotal = p.Subtotal,
                        Quantity = p.Quantity,
                        OrderId = p.OrderId,
                        OrderDate = p.OrderDate,
                        OrderStatus = p.OrderStatus,
                        UserName = p.UserName,
                        Amount = p.Amount,
                        Id = p.Id
                    }
                ).ToListAsync();

            return query;
        }

        public async Task<List<OrderDetailsViewModel>> GetByUser(string user)
        {
            var query = await _context.OrderDetails
                .Where(x => x.UserName == user)
                .Select(p =>
                    new OrderDetailsViewModel
                    {
                        ProductName = _context.Products.FirstOrDefault(c => c.IdProduct == p.IdProduct).ProductName.ToString(),
                        IdProduct = p.IdProduct,
                        Subtotal = p.Subtotal,
                        Quantity = p.Quantity,
                        OrderId = p.OrderId,
                        OrderDate = p.OrderDate,
                        OrderStatus = p.OrderStatus,
                        UserName = p.UserName,
                        Amount = p.Amount,
                        Id = p.Id
                    }
                ).ToListAsync();

            return query;
        }

        public async Task<OrderDetail> Save(OrderDetail order)
        {
            _context.OrderDetails.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<bool> Update(OrderDetail request)
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
            var model = await _context.OrderDetails.FindAsync(id);
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
