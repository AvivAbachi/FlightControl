using System.ComponentModel.DataAnnotations.Schema;

namespace FlightControl.Models
{
    public class Station
    {
        internal Action<object, Flight>? OnUpdate { get; set; }

        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual Flight? Flight { get; set; }
        [NotMapped]
        public Point Start { get; set; } = new Point();
        [NotMapped]
        public Point End { get; set; } = new Point();
   
        internal void Enter(Flight flight)
        {
            flight.Station?.Exit();
            flight.Station = this;
            Flight = flight;
            OnUpdate?.Invoke(this, flight);
        }

        internal virtual void Exit()
        {
            Flight = null;
        }
    }
}