using AirportDistanceCalc.Domain.Data;
using AirportDistanceCalc.Domain.Enum;
using AirportDistanceCalc.Domain.Models.Reports;
using AirportDistanceCalc.Domain.Models.Response;
using AirportDistanceCalc.Tests.Config;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace AirportDistanceCalc.Tests.Integration
{
    public class AirportControllerTests : IClassFixture<AirportDistanceCalcFactory<Program>>
    {
        private readonly HttpClient _client;

        public AirportControllerTests(AirportDistanceCalcFactory<Program> factory)
        {
            _client = factory.CreateClient();
            Utilities.RemoveDataDbForTests();
        }

        [Theory(DisplayName = "Should return ok")]
        [InlineData("ams", "bbb", UnitOfMeasureEnum.Miles)]
        [InlineData("aaa", "bbb", UnitOfMeasureEnum.Kilometers)]
        public async Task GET_WhenCalcIsOk_ReturnOK(string origin, string destination, UnitOfMeasureEnum unitOfMeasure)
        {
            //Arrange //Act
            var response = await _client.GetAsync($"/api/v1/Airport/CalcDistanceBetweenAirports?Origin={origin}&Destination={destination}&UnitOfMeasure={unitOfMeasure}");
            var content = await response.Content.ReadFromJsonAsync<Response>();

            //Assert
            content.StatusCode.Should().Be(StatusCodes.Status200OK);
            content.Data.Any().Should().BeTrue();
            content.Errors.Any().Should().BeFalse();
        }

        [Theory(DisplayName = "Should return not found when iata code are invalid")]
        [InlineData("zyz", "bbb", UnitOfMeasureEnum.Miles)]
        [InlineData("aaa", "zyz", UnitOfMeasureEnum.Kilometers)]
        public async Task GET_WhenAirportsNotFound_ReturnNotFound(string origin, string destination, UnitOfMeasureEnum unitOfMeasure)
        {
            //Arrange //Act
            var response = await _client.GetAsync($"/api/v1/Airport/CalcDistanceBetweenAirports?Origin={origin}&Destination={destination}&UnitOfMeasure={unitOfMeasure}");
            var content = await response.Content.ReadFromJsonAsync<Response>();

            //Assert
            content.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            content.Data.Any().Should().BeFalse();
            content.Errors.Any().Should().BeTrue();
        }

        [Fact(DisplayName = "Should return ok when try to get all of searches")]
        public async Task GET_WhenGetAllSearches_ReturnOk()
        {

            //Arrange
            Utilities.InitializeDbForTests();

            //Act
            var response = await _client.GetAsync($"/api/v1/Airport/GetAllSearches");
            var content = await response.Content.ReadFromJsonAsync<List<AirportVO>>();

            //Assert
            response.EnsureSuccessStatusCode().Should().HaveStatusCode(HttpStatusCode.OK);
            content.Any().Should().BeTrue();
        }

        [Fact(DisplayName = "Should return ok when try to get a report of most searched")]
        public async Task GET_WhenGetReportOfMostSearched_ReturnOk()
        {
            //Arrange
            Utilities.InitializeDbForTests();

            //Act
            var response = await _client.GetAsync($"/api/v1/Airport/GetReportOfMostSearched");
            var content = await response.Content.ReadFromJsonAsync<List<CityReport>>();

            //Assert
            response.EnsureSuccessStatusCode().Should().HaveStatusCode(HttpStatusCode.OK);
            content.Any().Should().BeTrue();
        }


        [Fact(DisplayName = "Should return not found when try to get all of searches")]
        public async Task GET_WhenGetAllSearches_ReturnNotFound()
        {
            //Arrange
            //Act
            var response = await _client.GetAsync($"/api/v1/Airport/GetAllSearches");
            var content = await response.Content.ReadFromJsonAsync<List<AirportVO>>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            content.Any().Should().BeFalse();
        }

        [Fact(DisplayName = "Should return not found when try to get a report of most searched")]
        public async Task GET_WhenGetReportOfMostSearched_ReturnNotFound()
        {
            //Arrange 
            //Act
            var response = await _client.GetAsync($"/api/v1/Airport/GetReportOfMostSearched");
            var content = await response.Content.ReadFromJsonAsync<List<AirportVO>>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            content.Any().Should().BeFalse();
        }

    }
}