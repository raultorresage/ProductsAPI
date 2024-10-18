using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductsAPI.Attributes;
using ProductsAPI.Data;
using ProductsAPI.Models;
using System.Text;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Tracker]
    public class UserController : ControllerBase
    {
        //private static List<User> Users = new List<User>();
        private readonly ApiDbContext _context;

        public UserController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpPost("login",Name = "LogInUser")]
        public async Task<IActionResult> LogIn([FromBody] User vU)
        {
           User? u = await this._context!.Users.FirstOrDefaultAsync(u => u.Username == vU.Username && u.Password == vU.Password);
            if (u == null)
            {
                return NotFound("User not found");
            }
            return Ok(u.GenerateJwtToken());
        }

        [HttpPost("register", Name = "RegisterUser")]
        public async Task<IActionResult> Register([FromBody] User vU)
        {
            User? result = await this._context.Users.FirstOrDefaultAsync(e => e.Username == vU.Username);
            if (result != null)
            {
                return BadRequest("User already exist");
            }
            try
            {
                await _context!.Users.AddAsync(vU);
                try
                {
                   await this._context.SaveChangesAsync();
                    return Ok(vU);
                } catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}

// Custom attribute on endpoint like a middleware to explicit log the endpoint is using
// On especific endpoint, on a method
// Middleware that look to the route and if it has this attribute prints something
