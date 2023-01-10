using FlightControl.Models;

namespace FlightControl.Api
{
    public class AirportConfig : IAirportConfig
    {
        private static readonly Station LandingA = new()
        {
            Name = "Langing A",
            Location = new Point { X = 850, Y = 64, R = 0 },
        };
        private static readonly Station LandingB = new()
        {
            Name = "Langing B",
            Location = new Point { X = 790, Y = 64, R = 0 },
        };
        private static readonly Station LandingC = new()
        {
            Name = "Langing C",
            Location = new Point { X = 730, Y = 64, R = 0 },
        };
        private static readonly Station Runaway = new()
        {
            Name = "Runaway",
            Location = new Point { X = 360, Y = 64, R = 0 },
        };
        private static readonly Station ToTeminals = new()
        {
            Name = "To Teminals",
            Location = new Point { X = 165, Y = 215, R = 225 },
        };
        private static readonly Station TeminalA = new()
        {
            Name = "Teminal A",
            Location = new Point { X = 286, Y = 290, R = 270 },
        };
        private static readonly Station TeminalB = new()
        {
            Name = "Teminal B",
            Location = new Point { X = 436, Y = 290, R = 270 },
        };
        private static readonly Station ToRunaway = new()
        {
            Name = "To Runaway",
            Location = new Point { X = 565, Y = 215, R = 130 },
        };
        private static readonly Station Takeoff = new()
        {
            Name = "Takeoff",
            Location = new Point { X = 0, Y = 64, R = 0 },
        };

        public Station[] Stations { get; } = new Station[] { LandingA, LandingB, LandingC, Runaway, ToTeminals, TeminalA, TeminalB, ToRunaway, Takeoff };
        public Station[] Terminals { get; } = new Station[] { TeminalA, TeminalB };

        private static readonly Station[] arrivalPath = new Station[] { LandingA, LandingB, LandingC, Runaway, ToTeminals };
        private static readonly Station[] departurePath = new Station[] { ToRunaway, Runaway, Takeoff };

        public Station[] GetPath(Flight flight)
        {
            switch (flight.Target)
            {
                case Target.Arrival: return arrivalPath;
                case Target.Departure: return departurePath;
                default: return Array.Empty<Station>();
            }
        }
    }
}
