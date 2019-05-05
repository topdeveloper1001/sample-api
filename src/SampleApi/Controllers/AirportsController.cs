using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Dto;
using SampleApi.Services;

namespace SampleApi.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    public class AirportsController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public AirportsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet("airports")]
        [ProducesResponseType(typeof(List<AirportDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAirports([FromQuery] string cityName)
        {
            var airports = await _flightService.GetAirports(cityName);
            return Ok(AirportDto.Map(airports));
        }

    }
}