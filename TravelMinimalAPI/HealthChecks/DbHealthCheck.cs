using Microsoft.Extensions.Diagnostics.HealthChecks;
using TravelMinimalAPI.DbContexts;

namespace TravelMinimalAPI.HealthChecks;

public class DbHealthCheck: IHealthCheck
{
    private readonly TravelDbContext _travelDbContext;

    public DbHealthCheck(TravelDbContext travelDbContext)
    {
        _travelDbContext = travelDbContext;
    }
    
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context
        , CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            _ = _travelDbContext.Destinations.ToList();
            _ = _travelDbContext.Activities.ToList();

            //throw new Exception("FUCK UP EXCEPTION"); 
            
            return Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception e)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy(null, e));
        }
    }
}