using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductsAPI.Attributes;
using ProductsAPI.Models;
using System.Text;

namespace ProductsAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Tracker]
    public class UserServices : ControllerBase
    {
        private static List<User> Users = new List<User>();

        [HttpPost("login",Name = "LogInUser")]
        
        public IActionResult LogIn([FromBody] User vU)
        {
            var user = Users.FirstOrDefault((u) =>
            
                u.Username.Equals(vU.Username) && u.Password.Equals(vU.Password)
            );

            if (user == null)
            {
               return NotFound("No user found");
            }
            string jwt = user.GenerateJwtToken("[k#%Yq~u1/*r1Oa%1!NN+TyF[$8Bs32/2Kjsko&%ci0jsdc", "products-issuer", "products-audience");
            return Ok(jwt);

        }

        [HttpPost("register", Name = "RegisterUser")]
        
        public IActionResult Register([FromBody] User vU)
        {
            var user = Users.FirstOrDefault((u) =>

                u.Username.Equals(vU.Username)
            );

            if (user != null)
            {
                return BadRequest("User already exist");
            }

            User newUser = new User(vU.Username, vU.Password);
            Users.Add(newUser);
            return Ok(newUser);
        }
    }
}

// Custom attribute on endpoint like a middleware to explicit log the endpoint is using
// On especific endpoint, on a method
// Middleware that look to the route and if it has this attribute prints something
