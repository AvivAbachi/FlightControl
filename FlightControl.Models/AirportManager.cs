using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlightControl.Models
{
    public class AirportManager
    {
        public static AirportManager GetInstance => instance;
        private static readonly AirportManager instance = new AirportManager();
        private readonly SemaphoreSlim safeBuffer = new SemaphoreSlim(1);

        private readonly Station PerLanding;
        private readonly StationBuffer Runaway;
        private readonly StationBuffer ToTermenals;
        private readonly StationBuffer TermenalA;
        private readonly StationBuffer ToTermenalB;
        private readonly StationBuffer TermenalB;
        private readonly StationBuffer ToRunaway;
        private readonly Station Out;

        private readonly Station[] pathA;
        private readonly Station[] pathB;
        private AirportManager()
        {
            PerLanding = new Station(1, "Per Landing", FlightUpdate);
            Runaway = new StationBuffer(2, "Runaway", FlightUpdate);
            ToTermenals = new StationBuffer(3, "To Termenals", FlightUpdate);
            TermenalA = new StationBuffer(4, "Termenal A", FlightUpdate);
            ToTermenalB = new StationBuffer(5, "To Termenal B", FlightUpdate);
            TermenalB = new StationBuffer(6, "Termenal B", FlightUpdate);
            ToRunaway = new StationBuffer(7, "To Runaway", FlightUpdate);
            Out = new Station(8, "Out", FlightUpdate);

            pathA = new Station[] { PerLanding, Runaway, ToTermenals, TermenalA, ToTermenalB, ToRunaway, Runaway, Out };
            pathB = new Station[] { PerLanding, Runaway, ToTermenals, ToTermenalB, TermenalB, ToRunaway, Runaway, Out };
        }

        public async Task AddFlight(Flight flight)
        {
            var path = GetPath(flight.Target);
            if (path == null) throw new ArgumentNullException(nameof(flight.Target));
            await flight.ProseecePlan(path, safeBuffer);
        }

        private Station[] GetPath(Target target)
        {
            switch (target)
            {
                case Target.TeminalA: return pathA;
                case Target.TeminalB: return pathB;
                default: return null;
            }
        }

        private void FlightUpdate(object sender, Flight flight)
        {
            OnFlightUpdate(sender, new FlightsArgs(flight));
        }
        public event EventHandler<FlightsArgs> OnFlightUpdate;
    }

    public class FlightsArgs : EventArgs
    {
        public Flight flight;
        public FlightsArgs(Flight flight)
        {
            this.flight = flight;
        }
    }
}
