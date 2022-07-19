using AirportDistanceCalc.Domain.Models.Request;
using AirportDistanceCalc.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirportDistanceCalc.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airportService;

        public AirportController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet("CalcDistanceBetweenAirports", Name = "CalcDistanceBetweenAirports")]
        public async Task<IActionResult> GetDistanceBetweenAirports([FromQuery] AirportCalcRequest airports)
        {
            var response = await _airportService.CalcDistanceBetweenAirports(airports);

            return !response.Errors.Any() ? Ok(response) : NotFound(response);
        }

        [HttpGet("GetAllSearches", Name = "GetAllSearches")]
        public async Task<IActionResult> GetAllSearches()
        {
            var response = await _airportService.GetAllSearches();
            return response.Any() ? Ok(response) : NotFound(response);
        }

        [HttpGet("GetReportOfMostSearched", Name = "GetReportOfMostSearched")]
        public async Task<IActionResult> GetReportOfMostSearchedByOrigin()
        {
            var response = await _airportService.GetReportOfMostSearched();
            return response.Any() ? Ok(response) : NotFound(response);
        } 
    }
}
