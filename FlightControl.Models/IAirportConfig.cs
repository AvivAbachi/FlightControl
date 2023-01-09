namespace FlightControl.Models
{
    public interface IAirportConfig
    {
        public  Station[] Stations { get; }
        public  Station[] Terminals { get; }
        public  Station[]? GetPath(Flight flight);
    }
}
