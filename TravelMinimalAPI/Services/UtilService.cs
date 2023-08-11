using TravelMinimalAPI.DbContexts;

namespace TravelMinimalAPI.Services;

public class UtilService
{
    private readonly TravelDbContext _travelDbContext;

    public UtilService(TravelDbContext travelDbContext)
    {
        _travelDbContext = travelDbContext;
    }
    
    public int GetNextDestinationId()
    {
        if (!_travelDbContext.Destinations.Any())
        {
            return 1;
        }

        return _travelDbContext.Destinations.Select(x => x.Id).Max() + 1;
    }
}