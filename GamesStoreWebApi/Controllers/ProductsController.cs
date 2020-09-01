using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamesStoreWebApi.Models;
using GamesStoreWebApi.ViewModels;
using GamesStoreWebApi.Paging;
using GamesStoreWebApi.Entities;
using Newtonsoft.Json;
using GamesStoreWebApi.RepositoryExtensions;
using GamesStoreWebApi.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace GamesStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _repository;

        public ProductsController(ProductRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Products
        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProducts([FromQuery] ProductParameters productParameters)
        {
            List<ProductViewModel> Listprod = await _repository.Get(productParameters);

            var products = PagedList<ProductViewModel>.ToPagedList(Listprod, productParameters.PageNumber, productParameters.PageSize);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));

            return Ok(products);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProductsCatalog([FromQuery] ProductParameters productParameters)
        {
            List<ProductViewModel> Listprod = await _repository.Get(productParameters);

            var products = PagedList<ProductViewModel>.ToPagedList(Listprod, productParameters.PageNumber, productParameters.PageSize);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));

            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProducts(int id)
        {
            var product = await _repository.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
            if (!ProductsExists(id))
            {
                return BadRequest();
            }
            bool result = await _repository.Update(products);
            if (result != true)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
            await _repository.Save(products);

            return CreatedAtAction("GetProducts", new { id = products.IdProduct }, products);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<Products>> DeleteProducts(int id)
        {
            if (!ProductsExists(id))
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

        private bool ProductsExists(int id)
        {
            var result = _repository.Get(id);
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
