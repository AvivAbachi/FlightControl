using System;

namespace FlightControl.Models
{
    public class Station
    {
        private readonly Action<object, Flight> OnUpdate;
        public int Id { get; }
        public string Name { get; }
        public Flight? Flight { get; set; }

        public Station(int id, string name, Action<object, Flight> onUpdate)
        {
            Id = id;
            Name = name;
            OnUpdate = onUpdate;
        }

        internal void Enter(Flight flight)
        {
            flight.Station?.Exit();
            flight.Station = this;
            Flight = flight;
            OnUpdate(this, flight);
        }

        internal virtual void Exit()
        {
            Flight = null;
        }
    }
}