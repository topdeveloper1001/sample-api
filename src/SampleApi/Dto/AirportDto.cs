using System.Collections.Generic;
using System.Linq;
using SampleApi.Domain;

namespace SampleApi.Dto
{
    public class AirportDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public static AirportDto Map(Airport airport)
        {
            return new AirportDto
            {
                Name = airport.Name,
                Code = airport.IataCode,
                Address = airport.Address,
                City = airport.CityName
            };
        }

        public static List<AirportDto> Map(IEnumerable<Airport> airports)
        {
            return airports.Select(Map).ToList();
        }
    }
}