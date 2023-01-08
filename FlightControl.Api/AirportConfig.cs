﻿using FlightControl.Models;

namespace FlightControl.Api
{
    public class AirportConfig : IAirportConfig
    {
        private static readonly Station LandingA = new()
        {
            Name = "Langing A",
        };
        private static readonly Station LandingB = new()
        {
            Name = "Langing B",
        };
        private static readonly Station LandingC = new()
        {
            Name = "Langing C",
        };
        private static readonly Station Runaway = new()
        {
            Name = "Runaway",
        };
        private static readonly Station ToTeminals = new()
        {
            Name = "To Teminals",
        };
        private static readonly Station TeminalA = new()
        {
            Name = "Teminal A",
        };
        private static readonly Station TeminalB = new()
        {
            Name = "Teminal B",
        };
        private static readonly Station ToRunaway = new()
        {
            Name = "To Runaway",
        };
        private static readonly Station Takeoff = new()
        {
            Name = "Takeoff",
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
