using FlightControl.Api.Data;
using FlightControl.Api.Repository;
using FlightControl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace FlightControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportManager manager;
        private readonly IAirportRepository repository;

        public AirportController(IAirportManager manager, IAirportRepository repository, IServiceScopeFactory serviceScopeFactory)
        {
            this.manager = manager;
            this.repository = repository;
            manager.OnFlightUpdate += (s, e) =>
            {
                using var scope = serviceScopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                //context?.Update(e.flight);
                //context?.SaveChanges();
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Flight?>> Get()
        {
            return Ok(new { Flights = manager.GetAllFlights(), Stations = manager.GetAllStations() });
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
