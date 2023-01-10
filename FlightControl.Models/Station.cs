using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlightControl.Models
{
    public class Station
    {
        private readonly SemaphoreSlim semaphore = new(1);
        internal Action<Flight>? OnUpdate { get; set; }

        [Key]
        public int StationId { get; set; }
        public string? Name { get; set; }
        public virtual List<Flight> Flights { get; set; } = new();
        [NotMapped]
        [JsonIgnore]
        public Point? Location { get; set; }

        internal void Enter(Flight flight)
        {
            flight.Station?.Exit(flight);
            flight.Station = this;
            if (Location != null) flight.Location.Set(Location);
            Flights.Add(flight);
            OnUpdate?.Invoke(flight);
        }

        internal async Task EnterAsync(Flight flight, CancellationToken cancellationToken = default)
        {
            await semaphore.WaitAsync(cancellationToken);
            Enter(flight);
        }

        internal virtual void Exit(Flight flight)
        {
            semaphore.Release();
            Flights.Remove(flight);
        }
    }
}