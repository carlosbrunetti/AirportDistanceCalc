using AirportDistanceCalc.Domain.Models;
using AirportDistanceCalc.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AirportDistanceCalc.Infrastructure.Context
{
    public class MemoryContext : DbContext
    {
        public MemoryContext(DbContextOptions<MemoryContext> options) : base(options) { }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AirportMapping.Map(modelBuilder);
            LocationMapping.Map(modelBuilder);
        }
    }
}
