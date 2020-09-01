using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamesStoreWebApi.Models;
using GamesStoreWebApi.ViewModels;
using GamesStoreWebApi.Repositories;

namespace GamesStoreWebApi.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailsRepository _repository;

        public OrderDetailsController(OrderDetailsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<List<OrderDetail>>> GetOrderDetails()
        {
            try
            {
                return await _repository.Get();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        // GET: api/OrderDetails/5
        [HttpGet("{orderid}")]
        public async Task<ActionResult<List<OrderDetailsViewModel>>> GetOrderDetail(int orderid)
        {
            var ListOrder = await _repository.GetByOrderId(orderid);

            if (ListOrder.Count() == 0)
            {
                return NotFound();
            }

            return ListOrder;
        }

        [Route("[action]/{username}")]
        [HttpGet]
        public async Task<ActionResult<List<OrderDetailsViewModel>>> GetOrderDetailByUser(string username)
        {
            var ListOrder = await _repository.GetByUser(username);

            if (ListOrder.Count() == 0)
            {
                return NotFound();
            }

            return ListOrder;
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (!OrderDetailExists(id))
            {
                return BadRequest();
            }
            bool result = await _repository.Update(orderDetail);
            if (result != true)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }

        // POST: api/OrderDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {

            await _repository.Save(orderDetail);

            return CreatedAtAction("GetByOrderId", new { id = orderDetail.OrderId }, orderDetail);

        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderDetail>> DeleteOrderDetail(int id)
        {
            if (!OrderDetailExists(id))
            {
                return NotFound();
            }

            bool result = await _repository.Delete(id);
            if (result != true)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        private bool OrderDetailExists(int id)
        {
            var result = _repository.GetByOrderId(id);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
