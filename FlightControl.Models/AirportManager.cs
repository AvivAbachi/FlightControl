namespace FlightControl.Models
{
    public class AirportManager : IAirportManager
    {
        public static AirportManager GetInstance => instance;
        private static readonly AirportManager instance = new();

        public event EventHandler<FlightsArgs> OnFlightUpdate;

        private readonly SemaphoreSlim safeBuffer = new(1);
        private readonly List<Flight> flights = new();

        private readonly Station[] all;
        private readonly Station[] pathA;
        private readonly Station[] pathB;

        private AirportManager()
        {
            var Waiting = new Station
            {
                Name = "Waitng",
                OnUpdate = FlightUpdate
            };
            var PerLanding = new StationBuffer
            {
                Name = "Per Landing",
                OnUpdate = FlightUpdate,
                Start = new Point { X = 700, Y = 50 },
                End = new Point { X = 600, Y = 50 }
            };
            var Runaway = new StationBuffer
            {
                Name = "Runaway",
                OnUpdate = FlightUpdate,
                Start = PerLanding.End,
                End = new Point { X = 150, Y = 50 }
            };
            var OutRunaway = new StationBuffer
            {
                Name = "Out Runaway",
                OnUpdate = FlightUpdate,
                Start = Runaway.End,
                End = new Point { X = 150, Y = 200 }
            };
            var ToTerminals = new StationBuffer
            {
                Name = "To Terminals",
                OnUpdate = FlightUpdate,
                Start = OutRunaway.End,
                End = new Point { X = 300, Y = 200 }
            };
            var TerminalA = new StationBuffer
            {
                Name = "Terminal A",
                OnUpdate = FlightUpdate,
                Start = ToTerminals.End,
                End = new Point { X = 300, Y = 300 }
            };
            var TerminalMidway = new StationBuffer
            {
                Name = "Terminal Midway",
                OnUpdate = FlightUpdate,
                Start = ToTerminals.End,
                End = new Point { X = 450, Y = 200 }
            };
            var TerminalB = new StationBuffer
            {
                Name = "Terminal B",
                OnUpdate = FlightUpdate,
                Start = TerminalMidway.End,
                End = new Point { X = 450, Y = 300 }
            };
            var OutTerminals = new StationBuffer
            {
                Name = "Out Terminals",
                OnUpdate = FlightUpdate,
                Start = TerminalMidway.End,
                End = new Point { X = 600, Y = 200 }
            };
            var ToRunaway = new StationBuffer
            {
                Name = "To Runaway",
                OnUpdate = FlightUpdate,
                Start = OutTerminals.End,
                End = Runaway.Start
            };
            var Takeoff = new StationBuffer
            {
                Name = "Takeoff",
                OnUpdate = FlightUpdate,
                Start = Runaway.End,
                End = new Point { X = 50, Y = 50 }
            };
            var Out = new Station
            {
                Name = "Out",
                OnUpdate = FlightUpdate,
                Start = Runaway.End,
            };
            all = new Station[] { Waiting, PerLanding, Runaway, OutRunaway, ToTerminals, TerminalA, TerminalMidway, TerminalB, OutTerminals, ToRunaway, Takeoff, Out };
            pathA = new Station[] { Waiting, PerLanding, Runaway, OutRunaway, ToTerminals, TerminalA, TerminalMidway, OutTerminals, ToRunaway, Runaway, Takeoff, Out };
            pathB = new Station[] { Waiting, PerLanding, Runaway, OutRunaway, ToTerminals, TerminalMidway, TerminalB, OutTerminals, ToRunaway, Runaway, Takeoff, Out };
        }

        public async Task AddFlight(Flight flight)
        {
            var path = GetPath(flight.Target);
            if (path == null) throw new ArgumentNullException("Target is not vaild", nameof(flight.Target));
            flights.Add(flight);
            await flight.ProseecePlan(path, safeBuffer);
            await Task.Delay(5000);
            flights.Remove(flight);
        }

        private Station[]? GetPath(Target target)
        {
            if (target == Target.TerminalA) return pathA;
            if (target == Target.TerminalB) return pathB;
            return null;
        }

        public IEnumerable<Station> GetAllStations()
        {
            return all;
        }

        public IEnumerable<Flight?> GetAllFlights()
        {
            return flights;
        }

        private void FlightUpdate(object sender, Flight flight)
        {
            OnFlightUpdate?.DynamicInvoke(sender, new FlightsArgs(flight));
        }
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
