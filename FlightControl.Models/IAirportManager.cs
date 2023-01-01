namespace FlightControl.Models
{
    public interface IAirportManager
    {
        event EventHandler<FlightsArgs> OnFlightUpdate;

        Task AddFlight(Flight flight);
        IEnumerable<Station> GetAllStations();
        IEnumerable<Flight?> GetAllFlights();
    }
}