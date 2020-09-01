using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesStoreWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly GameStoreContext _context;

        public AccountController(GameStoreContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Users.Add(model);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest(model);
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
