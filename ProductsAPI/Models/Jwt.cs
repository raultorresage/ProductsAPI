using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ProductsAPI.Models;

public class Jwt
{
    public static string? GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("[k#%Yq~u1/*r1Oa%1!NN+TyF[$8Bs32/2Kjsko&%ci0jsdc"));
        try
        {
            var claimsPrincipal = tokenHandler.ValidateToken(token, Config.tokenValParams, out var validatedToken);

            var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (userIdClaim == null)
            {
                Console.WriteLine("userIdClaim is null");
                return null;
            }

            return userIdClaim;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Handle validation failure
            return null;
        }
    }
}