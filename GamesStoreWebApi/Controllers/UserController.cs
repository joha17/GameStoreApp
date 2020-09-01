using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesStoreWebApi.Models;
using GamesStoreWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace GamesStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly GameStoreContext _context;

        public UserController(GameStoreContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{username}")]
        //[Authorize(Policy = Policies.User)]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            var products = await _context.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> PutUser(string username, User user)
        {
            if (username != user.UserName)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(username))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Postuser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserName }, user);
        }

        // DELETE: api/Emps/5
        [HttpDelete("{username}")]
        public async Task<ActionResult<User>> DeleteUser(string username)
        {
            var user = await _context.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(string username)
        {
            return _context.Users.Any(e => e.UserName == username);
        }
    }
}
