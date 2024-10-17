using Microsoft.IdentityModel.Tokens;
using ProductsAPI.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductsAPI.Models
{
    public class User
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

        public string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("[k#%Yq~u1/*r1Oa%1!NN+TyF[$8Bs32/2Kjsko&%ci0jsdc"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear claims con la información del objeto
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, this.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("userId", this.Id)
    };

            // Configurar el tiempo de expiración del token
            var token = new JwtSecurityToken(
                issuer: "products-issuer",
                audience: "products-audience",
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token expira en 'expireMinutes' minutos
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token); // return token
        }
    }
}
