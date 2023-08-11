using AutoMapper;
using TravelMinimalAPI.DTO;
using TravelMinimalAPI.Entities;

namespace TravelMinimalAPI.AutoMapperProfiles;

public class ActivityProfile: Profile
{
    public ActivityProfile()
    {
        CreateMap<Activity, ActivityDto>()
            .ForMember(
                x => x.DestinationId,
                x=>x.MapFrom(
                    y=>y.Destinations.First().Id));
    }
}