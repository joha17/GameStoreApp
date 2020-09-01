using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamesStoreWebApi.Models;
using GamesStoreWebApi.Migrations;

namespace GamesStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressesController : ControllerBase
    {
        private readonly GameStoreContext _context;

        public UserAddressesController(GameStoreContext context)
        {
            _context = context;
        }

        // GET: api/UserAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAddress>>> GetUserAddress()
        {
            return await _context.UserAddress.ToListAsync();
        }

        // GET: api/UserAddresses/5
        [HttpGet("{username}")]
        public async Task<ActionResult<UserAddress>> GetUserAddress(string username)
        {
            var userAddress = await _context.UserAddress.FindAsync(username);

            if (userAddress == null)
            {
                return NotFound();
            }

            return userAddress;
        }

        // PUT: api/UserAddresses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{username}")]
        public async Task<IActionResult> PutUserAddress(string username, UserAddress userAddress)
        {
            if (username != userAddress.UserName)
            {
                return BadRequest();
            }

            _context.Entry(userAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAddressExists(username))
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

        // POST: api/UserAddresses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserAddress>> PostUserAddress(UserAddress userAddress)
        {
            _context.UserAddress.Add(userAddress);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserAddressExists(userAddress.UserName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserAddress", new { username = userAddress.UserName }, userAddress);
        }

        // DELETE: api/UserAddresses/5
        [HttpDelete("{username}")]
        public async Task<ActionResult<UserAddress>> DeleteUserAddress(string username)
        {
            var userAddress = await _context.UserAddress.FindAsync(username);
            if (userAddress == null)
            {
                return NotFound();
            }

            _context.UserAddress.Remove(userAddress);
            await _context.SaveChangesAsync();

            return userAddress;
        }

        private bool UserAddressExists(string username)
        {
            return _context.UserAddress.Any(e => e.UserName == username);
        }
    }
}
