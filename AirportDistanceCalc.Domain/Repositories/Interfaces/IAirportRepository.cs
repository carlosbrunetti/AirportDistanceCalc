using AirportDistanceCalc.Domain.Models;

namespace AirportDistanceCalc.Domain.Repositories.Interfaces
{
    public interface IAirportRepository : IRepository<Airport>
    {
        Task<Airport> GetByIataCode(string iataCode);
    }
}
