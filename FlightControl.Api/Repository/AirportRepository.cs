using Duende.IdentityServer.Models;
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

        public void SetStations(IEnumerable<Station> stations)
        {
            context.Stations.AddRange(stations);
            context.SaveChanges();
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

        public IEnumerable<Flight> GetAllFlights(Target target)
        {
            return context.Flights.Where(f => f.Target == target).Include(f => f.Station)
;
        }
    }
}
