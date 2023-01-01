using System.Drawing;

namespace FlightControl.Models
{
    public class AirportManager : IAirportManager
    {
        public event EventHandler<FlightsArgs> OnFlightUpdate;

        public static AirportManager GetInstance => instance;
        private static readonly AirportManager instance = new();

        private readonly SemaphoreSlim safeBuffer = new(1);
        private readonly List<Flight> flights = new();

        private readonly Station[] all;
        private readonly Station[] pathA;
        private readonly Station[] pathB;

        private readonly Station Waiting;
        private readonly StationBuffer PerLanding;
        private readonly StationBuffer Runaway;
        private readonly StationBuffer OutRunaway;
        private readonly StationBuffer ToTerminals;
        private readonly StationBuffer TerminalA;
        private readonly StationBuffer TerminalMidway;
        private readonly StationBuffer TerminalB;
        private readonly StationBuffer OutTerminals;
        private readonly StationBuffer ToRunaway;
        private readonly Station Out;

        private AirportManager()
        {
            Waiting = new Station
            {
                Id = 1,
                Name = "Waitng",
                OnUpdate = FlightUpdate
            };
            PerLanding = new StationBuffer
            {
                Id = 2,
                Name = "Per Landing",
                OnUpdate = FlightUpdate,
                Start = new Point { X = 700, Y = 50 },
                End = new Point { X = 600, Y = 50 }
            };
            Runaway = new StationBuffer
            {
                Id = 3,
                Name = "Runaway",
                OnUpdate = FlightUpdate,
                Start = PerLanding.End,
                End = new Point { X = 150, Y = 50 }
            };
            OutRunaway = new StationBuffer
            {
                Id = 4,
                Name = "Out Runaway",
                OnUpdate = FlightUpdate,
                Start = Runaway.End,
                End = new Point { X = 150, Y = 200 }
            };
            ToTerminals = new StationBuffer
            {
                Id = 5,
                Name = "To Terminals",
                OnUpdate = FlightUpdate,
                Start = OutRunaway.End,
                End = new Point { X = 300, Y = 200 }
            };
            TerminalA = new StationBuffer
            {
                Id = 6,
                Name = "Terminal A",
                OnUpdate = FlightUpdate,
                Start = ToTerminals.End,
                End = new Point { X = 300, Y = 300 }
            };
            TerminalMidway = new StationBuffer
            {
                Id = 7,
                Name = "Terminal Midway",
                OnUpdate = FlightUpdate,
                Start = ToTerminals.End,
                End = new Point { X = 450, Y = 200 }
            };
            TerminalB = new StationBuffer
            {
                Id = 8,
                Name = "Terminal B",
                OnUpdate = FlightUpdate,
                Start = TerminalMidway.End,
                End = new Point { X = 450, Y = 300 }
            };
            OutTerminals = new StationBuffer
            {
                Id = 9,
                Name = "Out Terminals",
                OnUpdate = FlightUpdate,
                Start = TerminalMidway.End,
                End = new Point { X = 600, Y = 200 }
            };
            ToRunaway = new StationBuffer
            {
                Id = 10,
                Name = "To Runaway",
                OnUpdate = FlightUpdate,
                Start = OutTerminals.End,
                End = Runaway.Start
            };
            Out = new Station
            {
                Id = 11,
                Name = "Out",
                OnUpdate = FlightUpdate,
                Start = Runaway.End,
                End = new Point { X = 50, Y = 50 }
            };

            all = new Station[] { Waiting, PerLanding, Runaway, OutRunaway, ToTerminals, TerminalA, TerminalMidway, OutTerminals, TerminalB, ToRunaway, Out };
            pathA = new Station[] { Waiting, PerLanding, Runaway, OutRunaway, ToTerminals, TerminalA, TerminalMidway, OutTerminals, ToRunaway, Runaway, Out };
            pathB = new Station[] { Waiting, PerLanding, Runaway, OutRunaway, ToTerminals, TerminalMidway, TerminalB, OutTerminals, ToRunaway, Runaway, Out };
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
            return target switch
            {
                Target.TerminalA => pathA,
                Target.TerminalB => pathB,
                _ => null
            };
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
