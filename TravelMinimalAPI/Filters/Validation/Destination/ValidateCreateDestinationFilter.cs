using MiniValidation;
using TravelMinimalAPI.DTO;

namespace TravelMinimalAPI.Filters.Validation.Destination;

public class ValidateCreateDestinationFilter: IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.GetArgument<DestinationCreateDto>(3);

        if (!MiniValidator.TryValidate(model, out var errors))
        {
            return TypedResults.ValidationProblem(errors);
        }

        return await next(context);
    }
}