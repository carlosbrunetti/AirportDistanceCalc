using AirportDistanceCalc.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportDistanceCalc.Infrastructure.Mappings
{
    public class LocationMapping
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Latitude);
                x.Property(x => x.Longitude);
            });
        }
    }
}
