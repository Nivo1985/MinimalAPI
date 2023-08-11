using System.Net;

namespace TravelMinimalAPI.Filters;

public class CustomLogFilter: IEndpointFilter
{
    private readonly ILogger<CustomLogFilter> _logger;

    public CustomLogFilter(ILogger<CustomLogFilter> logger)
    {
        _logger = logger;
    }
    
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);
        var casterResult = result is INestedHttpResult httpResult ? httpResult.Result : (IResult)result!;

        if ((casterResult as IStatusCodeHttpResult)?.StatusCode == (int)HttpStatusCode.NotFound)
        {
            _logger.LogWarning($"{context.HttpContext.Request.Path} gave result of not found");
        }

        return result;
    }
}