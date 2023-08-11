using AutoMapper;
using TravelMinimalAPI.DTO;
using TravelMinimalAPI.Entities;

namespace TravelMinimalAPI.AutoMapperProfiles;

public class DestinationProfile: Profile
{
    public DestinationProfile()
    {
        CreateMap<Destination, DestinationDto>();
        CreateMap<DestinationCreateDto, Destination>();
        CreateMap<DestinationUpdateDto, Destination>();
    }
}