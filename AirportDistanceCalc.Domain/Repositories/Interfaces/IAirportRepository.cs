using AirportDistanceCalc.Domain.Models;
using AirportDistanceCalc.Domain.Models.Reports;

namespace AirportDistanceCalc.Domain.Repositories.Interfaces
{
    public interface IAirportRepository : IRepository<Airport>
    {
        Task<Airport> GetByIataCode(string iataCode);
        Task<List<Airport>> GetAllSearches();
        Task<List<CityReport>> GetReportOfMostSearched();
    }
}
