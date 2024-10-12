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
        public User LogIn([FromBody] string userName, [FromBody] string password)
        {
            var user = Users.FirstOrDefault((u) =>
            
                u.GetUserName() == userName && u.GetPassword() == password
            );

            if (user == null)
            {
                NotFound(userName);
                throw new Exception("No user found");
            }
            return user;
            
        }

        [HttpPost("register", Name = "RegisterUser")]
        public User Register([FromBody] string userName, [FromBody] string password)
        {
            var user = Users.FirstOrDefault((u) =>

                u.GetUserName() == userName && u.GetPassword() == password
            );

            if (user != null)
            {
                throw new Exception(
                    "User already Exist"
                    );
            }

            User newUser = new User(userName, password);
            Users.Add(newUser);
            return newUser;
        }
    }
}
