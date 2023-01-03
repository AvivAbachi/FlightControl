using FlightControl.Api.Data;
using FlightControl.Models;

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

        public Flight[] GetAllFlight()
        {
            return context.Flights.ToArray();
        }
    }
}
