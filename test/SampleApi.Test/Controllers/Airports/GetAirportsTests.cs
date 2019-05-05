using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using SampleApi.Dto;
using SampleApi.Services;
using Willow.Tests.Infrastructure;
using Workflow.Tests;
using Xunit;
using Xunit.Abstractions;

namespace SampleApi.Test.Controllers.Airports
{
    public class GetAirportsTests : BaseInMemoryTest
    {
        public GetAirportsTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task TripPinHasAirports_GetAirports_ReturnsAirports()
        {
            var expectedAirports = Fixture.CreateMany<AirportDto>(2);
            var tripPinAirports = expectedAirports.Select(x => new FlightService.GetAirportsResponse.Airport
            {
                Name = x.Name,
                IataCode = x.Code,
                Location = new FlightService.GetAirportsResponse.Location
                {
                    Address = x.Address,
                    City = new FlightService.GetAirportsResponse.City { Name = x.City }
                }
            }).ToList();

            using (var server = CreateServerFixture(ServerFixtureConfigurations.InMemoryDb))
            using (var client = server.CreateClient())
            {
                server.Arrange().GetTripPinService()
                    .SetupRequest(HttpMethod.Get, "Airports")
                    .ReturnsJson(new FlightService.GetAirportsResponse { Value = tripPinAirports });

                var response = await client.GetAsync("airports");

                response.StatusCode.Should().Be(HttpStatusCode.OK);
                var result = await response.Content.ReadAsAsync<List<AirportDto>>();
                result.Should().BeEquivalentTo(expectedAirports);
            }
        }

        [Fact]
        public async Task TripPinHasAirports_GetAirportsWithCityName_ReturnsAirportsOfTheGivenCity()
        {
            var cityName = "test city name";
            var expectedAirports = Fixture.Build<AirportDto>()
                                          .With(x => x.City, cityName)
                                          .CreateMany(2);
            var tripPinAirports = expectedAirports.Select(x => new FlightService.GetAirportsResponse.Airport
            {
                Name = x.Name,
                IataCode = x.Code,
                Location = new FlightService.GetAirportsResponse.Location
                {
                    Address = x.Address,
                    City = new FlightService.GetAirportsResponse.City { Name = x.City }
                }
            }).ToList();

            using (var server = CreateServerFixture(ServerFixtureConfigurations.InMemoryDb))
            using (var client = server.CreateClient())
            {
                server.Arrange().GetTripPinService()
                    .SetupRequest(HttpMethod.Get, $"Airports?$filter=Location/City/Name%20eq%20%27{WebUtility.UrlEncode(cityName)}%27")
                    .ReturnsJson(new FlightService.GetAirportsResponse { Value = tripPinAirports });

                var response = await client.GetAsync($"airports?cityName={cityName}");

                response.StatusCode.Should().Be(HttpStatusCode.OK);
                var result = await response.Content.ReadAsAsync<List<AirportDto>>();
                result.Should().BeEquivalentTo(expectedAirports);
            }
        }

    }
}