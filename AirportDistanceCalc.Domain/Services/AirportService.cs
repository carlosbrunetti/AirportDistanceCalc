using AirportDistanceCalc.Api.Config;
using AirportDistanceCalc.Domain.Data;
using AirportDistanceCalc.Domain.Enum;
using AirportDistanceCalc.Domain.Models.Request;
using AirportDistanceCalc.Domain.Models.Response;
using AirportDistanceCalc.Domain.Services.Interfaces;
using AutoMapper;
using FluentValidation;
using GeoCoordinatePortable;
using System.Net.Http.Headers;
using AirportDistanceCalc.Domain.Extension;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace AirportDistanceCalc.Domain.Services
{
    public class AirportService : IAirportService
    {
        private readonly IMapper _mapper;
        private readonly AirportAPI _airportAPI;
        private readonly IValidator<AirportCalcRequest> _validator;
        private Response _response;

        public AirportService(IMapper mapper, AirportAPI airportAPI, IValidator<AirportCalcRequest> validator)
        {
            _mapper = mapper;
            _airportAPI = airportAPI;
            _validator = validator;
            _response = new Response();
        }

        public async Task<Response> CalcDistanceBetweenAirports(AirportCalcRequest airports)
        {
            var result = await _validator.ValidateAsync(airports);

            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    _response.Errors.Add(item.PropertyName, item.ErrorMessage);
                }
                return _response;

            }

            var origin = await GetAirport(airports.Origin, EnumAirport.Origin);
            var destination = await GetAirport(airports.Destination, EnumAirport.Destination);

            if (origin is null || destination is null)
                return _response;

            var coordinateFrom = new GeoCoordinate(origin.location.lat, origin.location.lon);
            var coordinateTo = new GeoCoordinate(destination.location.lat, destination.location.lon);

            _response.StatusCode = StatusCodes.Status200OK;
            _response.Data.Add("Result", Extensions.ConvertMetersToMiles(coordinateFrom.GetDistanceTo(coordinateTo)));

            return _response;

        }

        private async Task<AirportVO?> GetAirport(string iata, EnumAirport enumAirport)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var airpotResponse = await client.GetAsync($"{_airportAPI.Url}/{iata.ToUpper()}");

            if (!airpotResponse.IsSuccessStatusCode)
            {
                _response.StatusCode = StatusCodes.Status404NotFound;
                _response.Errors.Add("Error", enumAirport == EnumAirport.Origin ? "Origin airport not found." : "Destination airport not found.");
                return null;
            }

            return await airpotResponse.Content.ReadFromJsonAsync<AirportVO>();
        }
    }
}
