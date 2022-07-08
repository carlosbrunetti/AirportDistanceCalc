using AirportDistanceCalc.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportDistanceCalc.Infrastructure.Mappings
{
    public class AirportMapping
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Iata).HasMaxLength(3);
                x.Property(x => x.Hubs);
                x.Property(x => x.Name);
                x.Property(x => x.Country);
                x.Property(x => x.City);
                x.Property(x => x.CountryIata);
                x.Property(x => x.CityIata);
                x.Property(x => x.TimeZoneRegionName);
                x.Property(x => x.CreatedIn);
                x.Property(x => x.Type);
                x.HasOne(x => x.AirportDestination);
                x.HasOne(x => x.Location)
                    .WithMany(x => x.Aiports)
                    .HasForeignKey(x => x.LocationId);

            });
        }
    }
}
