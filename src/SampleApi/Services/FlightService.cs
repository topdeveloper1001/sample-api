using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SampleApi.Domain;

namespace SampleApi.Services
{
    public interface IFlightService
    {
        Task<List<Airport>> GetAirports(string cityName);
    }

    public class FlightService : IFlightService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FlightService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Airport>> GetAirports(string cityName)
        {
            using (var client = _httpClientFactory.CreateClient("TripPin"))
            {
                var url = "airports";
                if (!string.IsNullOrEmpty(cityName))
                {
                    url += $"?$filter=Location/City/Name%20eq%20%27{WebUtility.UrlEncode(cityName)}%27";
                }

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsAsync<GetAirportsResponse>();
                var airports = result.Value.Select(a => new Airport
                {
                    Name = a.Name,
                    IataCode = a.IataCode,
                    Address = a.Location.Address,
                    CityName = a.Location.City.Name
                }).ToList();
                return airports;
            }
        }

        public class GetAirportsResponse
        {
            public List<Airport> Value { get; set; }

            public class Airport
            {
                public string Name { get; set; }
                public string IataCode { get; set; }
                public Location Location { get; set; }
            }

            public class Location
            {
                public string Address { get; set; }
                public City City { get; set; }
            }

            public class City
            {
                public string Name { get; set; }
            }
        }
    }
}
