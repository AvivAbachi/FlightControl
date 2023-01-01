using FlightControl.Api.Data;
using FlightControl.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightControl.Api.Repository
{
    public class AirportRepository : IAirportRepository
    {
        private readonly ApplicationDbContext context;

        public AirportRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddFlight(Flight flight)
        {
            context?.Add(flight);
            context?.SaveChanges();
        }

        public void UpdateFlight(Flight flight)
        {
            context?.Update(flight);
            context?.SaveChanges();
        }

        public void RemoveFlight(Flight flight)
        {
            context?.Remove(flight);
            context?.SaveChanges();
        }

        public Flight[] GetAllFlight()
        {
            return context.Flights.ToArray();
        }
    }
}
