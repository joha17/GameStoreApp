using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesStoreWebApi.Models;
using GamesStoreWebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewRepository _repository;

        public ReviewsController(ReviewRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Emps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            var reviews = await _repository.Get();
            return reviews;
        }

        [HttpGet]
        [Route("[action]/{productId}")]
        public async Task<ActionResult<List<Review>>> GetReviewsByProduct(int productId)
        {
            var reviews = await _repository.GetByProduct(productId);
            return reviews;
        }


        // GET: api/Emps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var emp = await _repository.Get(id);

            if (!ReviewExists(id))
            {
                return NotFound();
            }

            return emp;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Review review)
        {
            if (!ReviewExists(id))
            {
                return BadRequest();
            }
            bool result = await _repository.Update(review);
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
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            var username = await _repository.Get();
            if (username.Where(x => x.UserName == review.UserName).FirstOrDefault() != null)
            {
                var result = await _repository.Save(review);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Emps/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Review>> DeleteReview(int id)
        {
            if (!ReviewExists(id))
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

        private bool ReviewExists(int id)
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
