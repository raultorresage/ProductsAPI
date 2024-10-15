using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using ProductsAPI.Attributes;
using ProductsAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ProductsAPI.Filters
{
    public class AuthorizationFilter: IAuthorizationFilter
    {
        private readonly ILogger<AuthorizationFilter> _logger;

        public AuthorizationFilter (ILogger<AuthorizationFilter> logger)
        {
            this._logger = logger;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var attribute = context.ActionDescriptor.EndpointMetadata
            .OfType<Auth>()
            .FirstOrDefault();

            if (context.HttpContext.User.Identity?.IsAuthenticated != true && attribute != null)
            {
                var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogInformation($"No token provided by user {context.HttpContext.Connection.RemoteIpAddress?.ToString()} on {context.ActionDescriptor.RouteValues["controller"]}.{context.ActionDescriptor.RouteValues["action"]}");
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("[k#%Yq~u1/*r1Oa%1!NN+TyF[$8Bs32/2Kjsko&%ci0jsdc"); // Replace with your actual secret key

                try
                {
                    tokenHandler.ValidateToken(token, Config.tokenValParams, out SecurityToken validatedToken);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Invalid token provided by user {context.HttpContext.Connection.RemoteIpAddress?.ToString()} on {context.ActionDescriptor.RouteValues["controller"]}.{context.ActionDescriptor.RouteValues["action"]}: {ex.Message}");
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
        }
    }
}
