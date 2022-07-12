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
using AirportDistanceCalc.Domain.Repositories.Interfaces;
using AirportDistanceCalc.Domain.Models;
using AirportDistanceCalc.Domain.Models.Reports;

namespace AirportDistanceCalc.Domain.Services
{
    public class AirportService : IAirportService
    {
        private readonly IMapper _mapper;
        private readonly AirportAPI _airportAPI;
        private readonly IValidator<AirportCalcRequest> _validator;
        private readonly IAirportRepository _airportRepository;
        private Response _response;

        public AirportService(IMapper mapper, AirportAPI airportAPI, IValidator<AirportCalcRequest> validator, IAirportRepository airportRepository)
        {
            _mapper = mapper;
            _airportAPI = airportAPI;
            _validator = validator;
            _airportRepository = airportRepository;
            _response = new Response();
        }

        public async Task<List<AirportVO>> GetAllSearches()
        {
            return _mapper.Map<List<AirportVO>>(await _airportRepository.GetAllSearches());
        }

        public async Task<List<CityReport>> GetReportOfMostSearched()
        {
            return await _airportRepository.GetReportOfMostSearched();
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

            var origin = _mapper.Map<Airport>(await GetAirport(airports.Origin, EnumAirport.Origin));
            var destination = _mapper.Map<Airport>(await GetAirport(airports.Destination, EnumAirport.Destination));

            if (origin is null || destination is null)
                return _response;

            origin.AirportDestination = destination;
            SaveSearchHistory(origin);

            var coordinateFrom = new GeoCoordinate(origin.Location.Latitude, origin.Location.Longitude);
            var coordinateTo = new GeoCoordinate(destination.Location.Latitude, destination.Location.Longitude);

            var distance = Extensions.ConvertMetersTo(coordinateFrom.GetDistanceTo(coordinateTo),airports.UnitOfMeasure.Value);

            _response.StatusCode = StatusCodes.Status200OK;
            _response.Data.Add("Distance", distance);
            _response.Data.Add("Message", $"The distance between these airport is {distance} {airports.UnitOfMeasure.Value.Description()}.");

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

        private async void SaveSearchHistory(Airport origin)
        {
            await _airportRepository.Add(origin);
            await _airportRepository.SaveChanges();
        }
    }
}
