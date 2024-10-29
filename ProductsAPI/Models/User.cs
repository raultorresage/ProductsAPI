using Microsoft.IdentityModel.Tokens;
using ProductsAPI.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductsAPI.Models
{
    public interface IUser
    {
        public string Username { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        
    }
    public class User: IUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string username, string password)
        {
            this.Id = Guid.NewGuid().ToString("D");
            this.Username = username;
            this.Password = password;
        }
    }
}
