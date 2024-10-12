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
            
                u.GetUserName() == vU.GetUserName() && u.GetPassword() == vU.GetPassword()
            );

            if (user == null)
            {
                NotFound(vU);
                throw new Exception("No user found");
            }
            return Ok(user);
            
        }

        [HttpPost("register", Name = "RegisterUser")]
        public IActionResult Register([FromBody] User vU)
        {
            var user = Users.FirstOrDefault((u) =>

                u.GetUserName() == vU.GetUserName() && u.GetPassword() == vU.GetPassword()
            );

            if (user != null)
            {
                throw new Exception(
                    "User already Exist"
                    );
            }

            User newUser = new User(vU.GetUserName(), vU.GetPassword());
            Users.Add(newUser);
            return Ok(newUser);
        }
    }
}
