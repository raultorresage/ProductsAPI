using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class UserServices : ControllerBase
    {
        private static List<User> Users = new List<User>();

        [HttpPost("login",Name = "LogInUser")]      
        public IActionResult LogIn([FromBody] User vU)
        {
            var user = Users.FirstOrDefault((u) =>
            
                u.Username == vU.Username && u.Password == vU.Password
            );

            if (user == null)
            {
               return NotFound(vU);
            }
            return Ok(user);
            
        }

        [HttpPost("register", Name = "RegisterUser")]
        public IActionResult Register([FromBody] User vU)
        {
            var user = Users.FirstOrDefault((u) =>

                u.Username == vU.Username && u.Password == vU.Password
            );

            if (user != null)
            {
                return BadRequest(user);
            }

            User newUser = new User(vU.Username, vU.Password);
            Users.Add(newUser);
            return Ok(newUser);
        }
    }
}
