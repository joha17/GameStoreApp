using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamesStoreWebApi.Models;
using GamesStoreWebApi.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace GamesStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly CategoryRepository _repository;

        public CategoriesController(CategoryRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Emps
        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _repository.Get();
            return categories;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesCatalog()
        {
            var categories = await _repository.Get();
            return categories;
        }


        // GET: api/Emps/5
        [HttpGet("{id}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var emp = await _repository.Get(id);

            if (!CategoryExists(id))
            {
                return NotFound();
            }

            return emp;
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (!CategoryExists(id))
            {
                return BadRequest();
            }
            bool result = await _repository.Update( category);
            if (result != true)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _repository.Save(category);
            return CreatedAtAction("GetCategory", new { id = category.IdCategory }, category);
        }

        // DELETE: api/Emps/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            if (!CategoryExists(id))
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

        private bool CategoryExists(int id)
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
