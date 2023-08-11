namespace TravelMinimalAPI.DTO;

public class ActivityDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int DestinationId { get; set; }
}