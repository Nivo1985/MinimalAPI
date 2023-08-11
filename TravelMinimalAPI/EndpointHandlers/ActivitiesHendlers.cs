using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TravelMinimalAPI.DbContexts;
using TravelMinimalAPI.DTO;

namespace TravelMinimalAPI.EndpointHandlers;

public static class ActivitiesHandlers
{
    public static async Task<Results<Ok<IEnumerable<ActivityDto>>,NotFound>> GetActivities(TravelDbContext travelDbContext
        , IMapper mapper
        , ILogger<IEnumerable<ActivityDto>> logger 
        , int destinationId)
    {
        logger.Log(LogLevel.Information, "GetActivities");
        
        var destination = await travelDbContext.Destinations.Include(x => x.Activities)
            .FirstOrDefaultAsync(x => x.Id == destinationId);
        return destination == null ? TypedResults.NotFound() : TypedResults.Ok(mapper.Map<IEnumerable<ActivityDto>>(destination.Activities));
    }
}