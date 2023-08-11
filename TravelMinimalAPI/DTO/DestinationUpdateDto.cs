using System.ComponentModel.DataAnnotations;

namespace TravelMinimalAPI.DTO;

public class DestinationUpdateDto
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public required string Name { get; set; }
}