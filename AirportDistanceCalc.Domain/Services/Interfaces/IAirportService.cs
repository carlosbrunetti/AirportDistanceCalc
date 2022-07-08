using AirportDistanceCalc.Domain.Data;
using AirportDistanceCalc.Domain.Models.Request;
using AirportDistanceCalc.Domain.Models.Response;

namespace AirportDistanceCalc.Domain.Services.Interfaces
{
    public interface IAirportService
    {
        Task<List<AirportVO>> GetAll();
        Task<Response> CalcDistanceBetweenAirports(AirportCalcRequest airports);
    }
}
