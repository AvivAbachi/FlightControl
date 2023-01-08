namespace FlightControl.Models
{
    public class FlightsArgs : EventArgs
    {
        public Flight flight;
        public FlightsArgs(Flight flight)
        {
            this.flight = flight;
        }
    }
}
