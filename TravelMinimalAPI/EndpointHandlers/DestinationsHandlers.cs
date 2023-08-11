using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelMinimalAPI.DbContexts;
using TravelMinimalAPI.DTO;
using TravelMinimalAPI.Entities;

namespace TravelMinimalAPI.EndpointHandlers;

public static class DestinationsHandlers
{
    public static async Task<Ok<IEnumerable<DestinationDto>>> GetDestinations(TravelDbContext travelDbContext
        , IMapper mapper
        , ILogger<IEnumerable<DestinationDto>> logger)
    {
        logger.Log(LogLevel.Information, "GetDestinations");
        return TypedResults.Ok(mapper.Map<IEnumerable<DestinationDto>>(await travelDbContext.Destinations.ToListAsync()));
    }

    public static async Task<Results<Ok<DestinationDto>,NotFound>> GetDestinationsWithId(TravelDbContext travelDbContext
        , IMapper mapper
        , ILogger<DestinationDto> logger
        , int destinationId
        )
    {
        logger.Log(LogLevel.Information,"GetDestinationsWithId");
        var destination = await travelDbContext.Destinations.FirstOrDefaultAsync(x => x.Id == destinationId);
        return destination == null ? TypedResults.NotFound() : TypedResults.Ok(mapper.Map<DestinationDto>(destination));
    }
    
    public static async Task<Results<Ok<DestinationDto>,NotFound>> GetDestinationsWithName(TravelDbContext travelDbContext
        , IMapper mapper
        , ILogger<DestinationDto> logger
        , string destinationName)
    {
        logger.Log(LogLevel.Information, "GetDestinationsWithName");
        var destination = await travelDbContext.Destinations.FirstOrDefaultAsync(x => x.Name == destinationName);
        return destination == null ? TypedResults.NotFound() : TypedResults.Ok(mapper.Map<DestinationDto>(destination));
    }
    
    public static async Task<CreatedAtRoute<DestinationDto>> PostDestinations(TravelDbContext travelDbContext
        , IMapper mapper
        , ILogger<DestinationDto> logger
        , [FromBody]DestinationCreateDto destinationCreateDto)
    {
        logger.Log(LogLevel.Information, "PostDestinations");
        
        var destination = mapper.Map<Destination>(destinationCreateDto);
        travelDbContext.Add(destination);
        await travelDbContext.SaveChangesAsync();
        var result = mapper.Map<DestinationDto>(destination);
    
        return TypedResults.CreatedAtRoute(
            result,
            "GetDestination"
            ,new
            {
                destinationId = destination.Id
            }
        );
    }
    
    public static async Task<Results<NotFound,NoContent>> PutDestinationsWithId(TravelDbContext travelDbContext
        , IMapper mapper
        , ILogger<NoContent> logger
        , int destinationId
        , [FromBody] DestinationUpdateDto destinationUpdateDto)
    {
        logger.Log(LogLevel.Information, "PutDestinationsWithId");
        
        var destination = await travelDbContext.Destinations.FirstOrDefaultAsync(x => x.Id == destinationId);
        if (destination == null)
            return TypedResults.NotFound();
    
        mapper.Map(destinationUpdateDto, destination);
        await travelDbContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
    
    public static async Task<Results<NotFound,NoContent>> DeleteDestinationsWithId(TravelDbContext travelDbContext
        , ILogger<NoContent> logger
        , int destinationId)
    {
        logger.Log(LogLevel.Information, "DeleteDestinationsWithId");
        
        var destination = await travelDbContext.Destinations.FirstOrDefaultAsync(x => x.Id == destinationId);
        if (destination == null)
            return TypedResults.NotFound();

        travelDbContext.Destinations.Remove(destination);
        await travelDbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}