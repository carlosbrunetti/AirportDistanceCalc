using AirportDistanceCalc.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AirportDistanceCalc.Infrastructure.Context
{
    public class MemoryContext : DbContext
    {
        //public DbSet<AirportMapping> Airports { get; set; }
        //public DbSet<LocationMapping> Locations { get; set; }

        public MemoryContext(DbContextOptions<MemoryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AirportMapping.Map(modelBuilder);
            LocationMapping.Map(modelBuilder);
        }
    }
}
