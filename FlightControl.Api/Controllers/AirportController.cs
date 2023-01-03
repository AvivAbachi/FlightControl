using FlightControl.Api.Repository;
using FlightControl.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightControl.Api.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new { Flights = manager.GetAllFlights()});
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
