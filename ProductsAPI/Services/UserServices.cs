using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductsAPI.Data;
using ProductsAPI.Models;

namespace ProductsAPI.Services;

public class UserServices(ApiDbContext context)
{
    public async Task<User?> LogIn(User vU)
    {
        var u = await context!.Users.FirstOrDefaultAsync(u =>
            u.Username.Equals(vU.Username) && u.Password.Equals(vU.Password));
        return u;
    }

    public async Task<User?> Register(User vU)
    {
        var u = await context!.Users.FirstOrDefaultAsync(u => u.Username.Equals(vU.Username));
        if (u != null) return null;
        try
        {
            await context!.Users.AddAsync(vU);
            try
            {
                await context.SaveChangesAsync();
                return vU;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public string GenerateJwtToken(User user)
    {
        var securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("[k#%Yq~u1/*r1Oa%1!NN+TyF[$8Bs32/2Kjsko&%ci0jsdc"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Crear claims con la información del objeto
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("userId", user.Id)
        };

        // Configurar el tiempo de expiración del token
        var token = new JwtSecurityToken(
            "products-issuer",
            "products-audience",
            claims,
            expires: DateTime.Now.AddHours(1), // Token expira en 'expireMinutes' minutos
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token); // return token
    }
}