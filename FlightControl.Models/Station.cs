using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightControl.Models
{
    public class Station
    {
        [Key]
        public int StationId { get; set; }
        public string? Name { get; set; }
        public virtual List<Flight> Flights { get; set; } = new();
        [NotMapped]
        public Point Start { get; set; } = new Point();
        [NotMapped]
        public Point End { get; set; } = new Point();
        internal Action<object, Flight>? OnUpdate { get; set; }

        internal void Enter(Flight flight)
        {
            flight.Station?.Exit(flight);
            flight.Station = this;
            Flights.Add(flight);
            OnUpdate?.Invoke(this, flight);
        }

        internal virtual void Exit(Flight flight)
        {
            Flights.Remove(flight);
        }
    }
}