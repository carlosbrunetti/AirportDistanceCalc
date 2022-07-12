using AirportDistanceCalc.Domain.Models;
using AirportDistanceCalc.Domain.Models.Reports;
using AirportDistanceCalc.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirportDistanceCalc.Infrastructure.Repositories
{
    public class AirportRepository : Repository<Airport>, IAirportRepository
    {

        public AirportRepository(Context.MemoryContext context) : base(context) { }

        public async Task<Airport> GetByIataCode(string iataCode)
        {
            return await DbSet.Where(x => x.Iata == iataCode).FirstOrDefaultAsync();
        }
        public async Task<List<Airport>> GetAllSearches()
        {
            return await DbSet
                .Where(x => x.AirportDestination != null)
                .Include(x => x.Location)
                .Include(x => x.AirportDestination)
                .ToListAsync();
        }

        public async Task<List<CityReport>> GetReportOfMostSearched()
        {

            return await DbSet
                .Include(x => x.AirportDestination)
                .Where(x => x.AirportDestination != null)
                .GroupBy(x => new
                {
                    OriginCountry = x.Country,
                    OriginCity = x.City,
                    OriginAirportName = x.Name,
                    DestinationCity = x.AirportDestination.City,
                    DestinationCountry = x.AirportDestination.Country,
                    DestinationAirportName = x.AirportDestination.Name

                })
                .Select(x => new CityReport
                {
                    OriginCountry = x.Key.OriginCountry,
                    OriginCity = x.Key.OriginCity,
                    OriginAirportName = x.Key.OriginAirportName,
                    DestinationCountry = x.Key.DestinationCountry,
                    DestinationCity = x.Key.DestinationCity,
                    DestinationAirportName = x.Key.DestinationAirportName,
                    Count = x.Count()

                })
                .OrderByDescending(x => x.Count)
                .ToListAsync();
        }
    }
}
