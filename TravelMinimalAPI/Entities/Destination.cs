using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TravelMinimalAPI.Entities;

public class Destination
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    public ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public Destination()
    {  }

    [SetsRequiredMembers]
    public Destination(int id, string name)
    {
        Id = id;
        Name = name;
    }
}