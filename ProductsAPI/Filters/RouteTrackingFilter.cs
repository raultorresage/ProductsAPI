using Microsoft.AspNetCore.Mvc.Filters;
using ProductsAPI.Attributes;

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

        if (attribute != null) _logger.LogInformation($"Entered on: {context.HttpContext.GetEndpoint()}");

        await next();
    }
}