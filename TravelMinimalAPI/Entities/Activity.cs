using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TravelMinimalAPI.Entities;

public class Activity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    public ICollection<Destination> Destinations { get; set; } = new List<Destination>();

    public Activity()
    { 
    }

    [SetsRequiredMembers]
    public Activity(int id, string name)
    {
        Id = id;
        Name = name;
    }
}