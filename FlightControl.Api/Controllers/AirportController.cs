using FlightControl.Api.Repository;
using FlightControl.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightControl.Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportManager manager;
        private readonly IAirportRepository repository;

        public AirportController(IAirportManager manager, IAirportRepository repository)
        {
            this.manager = manager;
            this.repository = repository;
        }

        [HttpGet("Arrival")]
        public ActionResult GetArrival()
        {
            return Ok(new { Flights = repository.GetAllFlights(Target.Arrival), Map = manager.GetAllStations().SelectMany(st => st.Flights) });
        }

        [HttpGet("Departure")]
        public ActionResult GetDeparture()
        {
            return Ok(new { Flights = repository.GetAllFlights(Target.Departure), Map = manager.GetAllStations().SelectMany(st => st.Flights) });
        }


        [HttpPost]
        public ActionResult Post([FromBody] Flight flight)
        {
            if (flight == null)
            {
                return BadRequest();
            }
            repository.AddFlight(flight);
            manager.AddFlight(flight);

            return Ok();
        }
    }
}
