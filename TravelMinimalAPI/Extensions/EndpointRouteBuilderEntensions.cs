using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using TravelMinimalAPI.EndpointHandlers;
using TravelMinimalAPI.Filters;
using TravelMinimalAPI.Filters.Validation.Destination;

namespace TravelMinimalAPI.Extensions;

public static class EndpointRouteBuilderEntensions
{
    public static void RegisterEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var destinationsEndpoints = endpointRouteBuilder.MapGroup("/destinations");//.RequireAuthorization();
        var destinationsWithIdEndpoints = destinationsEndpoints.MapGroup("/{destinationId:int}");
        var destinationsWithNameEndpoints = destinationsEndpoints.MapGroup("/{destinationName}");
        var activitiesEndpoints = destinationsWithIdEndpoints.MapGroup("/activities");

        endpointRouteBuilder.MapGet("/", Ok<bool> (ClaimsPrincipal claimsPrincipal) => TypedResults.Ok(claimsPrincipal.Identity is { IsAuthenticated: true }))
            .WithOpenApi(configureOperation =>
            {
                configureOperation.Deprecated = true;
                
                return configureOperation;
            });
        destinationsEndpoints.MapGet("",  DestinationsHandlers.GetDestinations);
        destinationsWithIdEndpoints.MapGet("", DestinationsHandlers.GetDestinationsWithId)
            .AddEndpointFilter<CustomLogFilter>()
            .WithName("GetDestination")
            .WithOpenApi(configureOperation =>
            {
                configureOperation.Summary = "Endpoint summary";
                configureOperation.Description = "Endpoint description";
                return configureOperation;
            });
            // .WithSummary("Endpoint summary")
            // .WithDescription("Endpoint description");
        destinationsWithNameEndpoints.MapGet("", DestinationsHandlers.GetDestinationsWithName);
        activitiesEndpoints.MapGet("", ActivitiesHandlers.GetActivities);
        destinationsEndpoints.MapPost("", DestinationsHandlers.PostDestinations)
            .RequireAuthorization("UEAdmin")
            .AddEndpointFilter<ValidateCreateDestinationFilter>();
        destinationsWithIdEndpoints.MapPut("", DestinationsHandlers.PutDestinationsWithId)
            .RequireAuthorization("UEAdmin")
            .AddEndpointFilter(new DestinationFrozenFilter(1))
            .AddEndpointFilter(new DestinationFrozenFilter(2))
            .AddEndpointFilter<ValidateUpdateDestinationFilter>();
        destinationsWithIdEndpoints.MapDelete("", DestinationsHandlers.DeleteDestinationsWithId)
            .RequireAuthorization("UEAdmin")
            .AddEndpointFilter(new DestinationFrozenFilter(1))
            .AddEndpointFilter(new DestinationFrozenFilter(2));
        
        // destinationsWithIdEndpoints.MapDelete("", DestinationsHandlers.DeleteDestinationsWithId)
        //     .AddEndpointFilter<DestinationFrozen>();
    }
}