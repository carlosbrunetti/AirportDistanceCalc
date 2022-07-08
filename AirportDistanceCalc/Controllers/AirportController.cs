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

        [HttpPost(Name = "CalcDistanceBetweenAirports")]
        public async Task<IActionResult> GetDistanceBetweenAirports([FromQuery] AirportCalcRequest airports)
        {
            var response = await _airportService.CalcDistanceBetweenAirports(airports);

            return !response.Errors.Any() ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _airportService.GetAll());

            //return !response.Errors.Any() ? Ok(response) : BadRequest(response);
        }
    }
}
