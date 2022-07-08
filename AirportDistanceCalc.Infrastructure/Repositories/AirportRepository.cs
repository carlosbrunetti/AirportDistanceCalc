using AirportDistanceCalc.Domain.Models;
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
    }
}
