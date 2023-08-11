using Microsoft.EntityFrameworkCore;
using TravelMinimalAPI.Entities;

namespace TravelMinimalAPI.DbContexts;

public class TravelDbContext: DbContext
{
    public DbSet<Destination> Destinations { get; set; } = null!;
    public DbSet<Activity> Activities { get; set; } = null!;
    
    public TravelDbContext(DbContextOptions<TravelDbContext> options):base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Destination>().HasData(
            new (1, "Greece"),
            new (2,"Spain"),
            new (3, "Germany"),
            new (4, "Poland"));

        modelBuilder.Entity<Activity>().HasData(
            new (1,"Nice weather"),
            new (2,"Good food"),
            new (3,"Castles"),
            new (4,"Nice People"),
            new (5,"Good beer"),
            new (6, "Good wine"));

        modelBuilder
            .Entity<Destination>()
            .HasMany(x => x.Activities)
            .WithMany(x => x.Destinations)
            .UsingEntity(x => x.HasData(
                new {DestinationsId = 1, ActivitiesId =1},
                new {DestinationsId = 1, ActivitiesId =2},
                new {DestinationsId = 1, ActivitiesId =4},
                new {DestinationsId = 1, ActivitiesId =6},
                new {DestinationsId = 2, ActivitiesId =1},
                new {DestinationsId = 2, ActivitiesId =2},
                new {DestinationsId = 2, ActivitiesId =3},
                new {DestinationsId = 2, ActivitiesId =4},
                new {DestinationsId = 3, ActivitiesId =3},
                new {DestinationsId = 3, ActivitiesId =5},
                new {DestinationsId = 4, ActivitiesId =2},
                new {DestinationsId = 4, ActivitiesId =3},
                new {DestinationsId = 4, ActivitiesId =4},
                new {DestinationsId = 4, ActivitiesId =5}
                ));
            
        base.OnModelCreating(modelBuilder);
    }
}