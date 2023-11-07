using Microsoft.EntityFrameworkCore;
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
    
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var destinationsTask = _travelDbContext.Destinations.ToListAsync(cancellationToken);
            var activitiesTask = _travelDbContext.Activities.ToListAsync(cancellationToken);

            await Task.WhenAll(destinationsTask, activitiesTask);

            return HealthCheckResult.Healthy();
        }
        catch (Exception e)
        {
            return HealthCheckResult.Unhealthy(null, e);
        }
    }
}