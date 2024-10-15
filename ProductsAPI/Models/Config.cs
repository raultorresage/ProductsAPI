using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProductsAPI.Models
{
    public class Config
    {
        public static TokenValidationParameters tokenValParams = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = "products-issuer",
    ValidAudience = "products-audience",
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("[k#%Yq~u1/*r1Oa%1!NN+TyF[$8Bs32/2Kjsko&%ci0jsdc"))
};
}
}
