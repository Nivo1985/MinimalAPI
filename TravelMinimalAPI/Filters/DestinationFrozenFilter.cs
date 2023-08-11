namespace TravelMinimalAPI.Filters;

public class DestinationFrozenFilter: IEndpointFilter
{
    private readonly int _destinationId;
    
    public DestinationFrozenFilter(int destinationId)
    {
        _destinationId = destinationId;
    }
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var destinationId = int.Parse(context.HttpContext.GetRouteValue("destinationId")?.ToString() ?? string.Empty) ;

        if (destinationId ==_destinationId)
        {
            return TypedResults.Problem(new()
            {
                Status = 400,
                Title = "This element can't be modified"
            });
        }
        return await next.Invoke(context);
    }
}