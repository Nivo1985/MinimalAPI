using FastEndpoints;
using FastEndPointsDemo.DTO;

namespace FastEndPointsDemo.Endpoints;

public class GetInfoEndpoint : EndpointWithoutRequest<InfoResponse>
{
    public override void Configure()
    {
        Get("/info");
        AllowAnonymous();
        Summary(s => s.Summary = "Get information about the API");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = new InfoResponse
        {
            Info = "This is a demo API using FastEndpoints."
        };
        
        await SendAsync(response, cancellation: ct);
    }
}