using AirportDistanceCalc.Api.Config;
using AirportDistanceCalc.Api.Config.AutoMapper;
using AirportDistanceCalc.Domain.Models;
using AirportDistanceCalc.Domain.Models.Request;
using AirportDistanceCalc.Domain.Repositories.Interfaces;
using AirportDistanceCalc.Domain.Services;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AirportDistanceCalc.Tests.Unit.Services
{
    public class AirportServiceTest
    {
        Mock<IAirportRepository> _airportRepositoryMock;
        Mock<IValidator<AirportCalcRequest>> _airportCalcRequestMock;
        Mock<AirportAPI> _airportAPIMock;

        AirportService _airportService;


        public AirportServiceTest()
        {
            _airportRepositoryMock = new Mock<IAirportRepository>();
            _airportCalcRequestMock = new Mock<IValidator<AirportCalcRequest>>();
            _airportAPIMock = new Mock<AirportAPI>();

            var mapper = MappingConfig.RegisterMaps().CreateMapper();

            InitializeAiportService(mapper);
        }

        [Fact]
        public async Task Teste()
        {
            //ARRANGE
            var airports = PopulateAirports();
            _airportRepositoryMock.Setup(x => x.GetAllSearches()).ReturnsAsync(airports);

            //ACT
            var result = await _airportService.GetAllSearches();

            //ASSERT
            result.Should().HaveCount(1);

        }

        [Fact(DisplayName = "Should return empty when try to get all searches")]
        public async Task Teste1()
        {
            //ARRANGE
            _airportRepositoryMock.Setup(x => x.GetAllSearches()).ReturnsAsync(new List<Airport>());

            //ACT
            var result = await _airportService.GetAllSearches();

            //ASSERT
            result.Should().HaveCount(0);

        }

        private void InitializeAiportService(IMapper mapper)
        {
            _airportService = new AirportService(mapper, _airportAPIMock.Object, _airportCalcRequestMock.Object, _airportRepositoryMock.Object);
        }

        private List<Airport> PopulateAirports()
        {
            return new List<Airport>()
            {
                new Airport
                {
                    Country = "Netherlands",
                    CityIata = "AMS",
                    Iata = "AMS",
                    City = "Amsterdam",
                    TimeZoneRegionName = "Europe/Amsterdam",
                    CountryIata = "NL",
                    Rating = 3,
                    Name = "Amsterdam",
                    Location = new Location
                    {
                        Longitude = 4.763385,
                        Latitude =  52.309069
                    },
                    Type = "airport",
                    Hubs = 7,
                    AirportDestination = new Airport
                    {
                        Country = "Portugal",
                        CityIata = "LIS",
                        Iata = "LIS",
                        City = "Lisbon",
                        TimeZoneRegionName = "Europe/Lisbon",
                        CountryIata = "PT",
                        Rating = 3,
                        Name = "Lisbon",
                        Location = new Location
                        {
                            Longitude = -9.128165,
                            Latitude =  38.770043
                        },
                        Type = "airport",
                        Hubs = 5
                    }
                }
            };
        }

    }
}
