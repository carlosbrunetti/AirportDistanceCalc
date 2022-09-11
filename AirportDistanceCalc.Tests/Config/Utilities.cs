using AirportDistanceCalc.Domain.Models;
using AirportDistanceCalc.Infrastructure.Context;
using System.Collections.Generic;

namespace AirportDistanceCalc.Tests.Config
{
    public  class Utilities
    {
        protected static MemoryContext _memoryContext;

        public static void SetDbContext(MemoryContext db)
        {
            _memoryContext = db;
        }

        public static void InitializeDbForTests()
        {
            _memoryContext.Database.EnsureCreated();
            _memoryContext.Airports.AddRange(GetSeedingFamiliess());
            _memoryContext.SaveChanges();
        }

        private static Airport[] GetSeedingFamiliess()
        {
            return new List<Airport>
            {
                new Airport
                {
                    Country = "Netherlands",
                    CityIata = "AMS",
                    Iata = "AMS",
                    City = "Amsterdam",
                    TimeZoneRegionName = "Europe/Amsterdam",
                    CountryIata = "NL",
                    Rating = 3,
                    Name = "Amsterdam",
                    Location = new Location
                    {
                        Longitude = 4.763385,
                        Latitude =  52.309069
                    },
                    Type = "airport",
                    Hubs = 7,
                    AirportDestination = new Airport
                    {
                        Country = "Portugal",
                        CityIata = "LIS",
                        Iata = "LIS",
                        City = "Lisbon",
                        TimeZoneRegionName = "Europe/Lisbon",
                        CountryIata = "PT",
                        Rating = 3,
                        Name = "Lisbon",
                        Location = new Location
                        {
                            Longitude = -9.128165,
                            Latitude =  38.770043
                        },
                        Type = "airport",
                        Hubs = 5
                    }
                }

            }.ToArray();

        }

        public static void RemoveDataDbForTests()
        {
            _memoryContext.Airports.RemoveRange(_memoryContext.Airports);
            _memoryContext.SaveChanges();
        }
    }
}
