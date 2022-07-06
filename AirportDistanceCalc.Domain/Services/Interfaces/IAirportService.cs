using AirportDistanceCalc.Domain.Models.Request;
using AirportDistanceCalc.Domain.Models.Response;

namespace AirportDistanceCalc.Domain.Services.Interfaces
{
    public interface IAirportService
    {
        Task<Response> CalcDistanceBetweenAirports(AirportCalcRequest airports);
    }
}
