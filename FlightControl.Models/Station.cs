using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public Point Start { get; set; }
        [NotMapped]
        public Point End { get; set; }

        internal async Task Enter(Flight flight, CancellationToken cancellationToken = default)
        {
            await semaphore.WaitAsync(cancellationToken);
            flight.Station?.Exit(flight);
            flight.Station = this;
            Flights.Add(flight);
            OnUpdate?.Invoke(flight);
        }

        internal virtual void Exit(Flight flight)
        {
            semaphore.Release();
            Flights.Remove(flight);
        }

        internal async Task Block()
        {
            await semaphore.WaitAsync();
            await Task.Delay(1000);
            semaphore.Release();
        }
    }
}