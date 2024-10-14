using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ProductsAPI.Attributes;
using System.Threading.Tasks;

public class RouteTrackingFilter : IAsyncActionFilter
{
    private readonly ILogger<RouteTrackingFilter> _logger;

    public RouteTrackingFilter(ILogger<RouteTrackingFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var attribute = context.ActionDescriptor.EndpointMetadata
            .OfType<Tracker>()
            .FirstOrDefault();

        if (attribute != null)
        {
            _logger.LogInformation($"Entered on: {attribute.Route}");
        }

        await next(); 
    }
}
