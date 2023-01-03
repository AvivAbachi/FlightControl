using FlightControl.Models;

namespace FlightControl.Api.Repository
{
    public interface IAirportRepository
    {
        void SetStations(IEnumerable<Station> stations);
        void AddFlight(Flight flight);
        Flight[] GetAllFlight();
        void RemoveFlight(Flight flight);
        void UpdateFlight(Flight flight);
    }
}