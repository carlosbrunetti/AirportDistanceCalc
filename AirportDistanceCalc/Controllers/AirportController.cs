using AirportDistanceCalc.Domain.Models.Request;
using AirportDistanceCalc.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirportDistanceCalc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {

        private readonly IAirportService _airportService;

        public AirportController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] AirportCalcRequest airports)
        {
            var response = await _airportService.CalcDistanceBetweenAirports(airports);

            return !response.Errors.Any() ? Ok(response) : BadRequest(response);
        }
    }
}
