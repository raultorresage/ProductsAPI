using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductsAPI.Attributes;

namespace ProductsAPI.Filters
{
    public class AuthorizationFilter: IAuthorizationFilter
    {
        private readonly ILogger<AuthorizationFilter> _logger;

        public AuthorizationFilter (ILogger<AuthorizationFilter> logger)
        {
            this._logger = logger;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var attribute = context.ActionDescriptor.EndpointMetadata
            .OfType<Auth>()
            .FirstOrDefault();

            if (context.HttpContext.User.Identity?.IsAuthenticated != true && attribute != null)
            {
                this._logger.LogInformation($"User {context.HttpContext.Connection.RemoteIpAddress?.ToString()} is not authenticated on {context.ActionDescriptor.RouteValues["controller"]}.{context.ActionDescriptor.RouteValues["action"]}");
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
