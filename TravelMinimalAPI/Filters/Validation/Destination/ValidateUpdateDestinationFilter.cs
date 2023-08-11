using MiniValidation;
using TravelMinimalAPI.DTO;

namespace TravelMinimalAPI.Filters.Validation.Destination;

public class ValidateUpdateDestinationFilter: IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.GetArgument<DestinationUpdateDto>(4);

        if (!MiniValidator.TryValidate(model, out var errors))
        {
            return TypedResults.ValidationProblem(errors);
        }

        return await next(context);
    }
}