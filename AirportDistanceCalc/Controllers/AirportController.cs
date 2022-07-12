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

            return !response.Errors.Any() ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetAllSearches", Name = "GetAllSearches")]
        public async Task<IActionResult> GetAllSearches()
        {
            return Ok(await _airportService.GetAllSearches());
        }

        [HttpGet("GetReportOfMostSearched", Name = "GetReportOfMostSearched")]
        public async Task<IActionResult> GetReportOfMostSearchedByOrigin()
        {
            return Ok(await _airportService.GetReportOfMostSearched());
        } 
        
    }
}
