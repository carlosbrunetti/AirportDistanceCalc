using AirportDistanceCalc.Domain.Data;
using AirportDistanceCalc.Domain.Models.Reports;
using AirportDistanceCalc.Domain.Models.Request;
using AirportDistanceCalc.Domain.Models.Response;

namespace AirportDistanceCalc.Domain.Services.Interfaces
{
    public interface IAirportService
    {
        Task<List<AirportVO>> GetAllSearches();
        Task<Response> CalcDistanceBetweenAirports(AirportCalcRequest airports);
        Task<List<CityReport>> GetReportOfMostSearched();
    }
}
