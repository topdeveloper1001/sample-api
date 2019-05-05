using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using MaxKagamine.Moq.HttpClient;
using SampleApi.Dto;
using SampleApi.Services;
using Willow.Tests.Infrastructure;
using Workflow.Tests;
using Xunit;
using Xunit.Abstractions;

namespace SampleApi.Test.Controllers.Airports
{
    public class GetAirportTests : BaseInMemoryTest
    {
        public GetAirportTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task AirportExists_GetAirport_ReturnsAirport()
        {
            var expectedAirport = Fixture.Create<AirportDto>();
            var tripPinAirport = new FlightService.GetAirportsResponse.Airport
            {
                Name = expectedAirport.Name,
                IataCode = expectedAirport.Code,
                Location = new FlightService.GetAirportsResponse.Location
                {
                    Address = expectedAirport.Address,
                    City = new FlightService.GetAirportsResponse.City { Name = expectedAirport.City }
                }
            };

            using (var server = CreateServerFixture(ServerFixtureConfigurations.InMemoryDb))
            using (var client = server.CreateClient())
            {
                server.Arrange().GetTripPinService()
                    .SetupRequest(HttpMethod.Get, $"Airports/{expectedAirport.Code}")
                    .ReturnsJson(tripPinAirport);

                var response = await client.GetAsync($"airports/{expectedAirport.Code}");

                response.StatusCode.Should().Be(HttpStatusCode.OK);
                var result = await response.Content.ReadAsAsync<AirportDto>();
                result.Should().BeEquivalentTo(expectedAirport);
            }
        }

        [Fact]
        public async Task AirportDoesNotExist_GetAirport_ReturnsNotFound()
        {
            var airportCode = "codecode";
            using (var server = CreateServerFixture(ServerFixtureConfigurations.InMemoryDb))
            using (var client = server.CreateClient())
            {
                server.Arrange().GetTripPinService()
                    .SetupRequest(HttpMethod.Get, $"Airports/{airportCode}")
                    .ReturnsResponse(HttpStatusCode.NotFound);

                var response = await client.GetAsync($"airports/{airportCode}");

                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}