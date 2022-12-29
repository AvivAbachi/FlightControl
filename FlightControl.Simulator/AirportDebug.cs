using FlightControl.Models;
using System.Diagnostics;

namespace FlightControl.Simulator
{
    public class AirportDebug
    {
        private static readonly AirportManager airport = AirportManager.GetInstance;
        static public void Debug()
        {
            airport.OnFlightUpdate += (o, e) =>
            {
                Console.WriteLine($"{e.flight.FlightId}\t{(o as Station)?.Name}");
            };

            var flights = new Flight[]
            {
                new Flight { Target = Target.TeminalA, FlightId = 1 },
                new Flight { Target = Target.TeminalB, FlightId = 2 },
                new Flight { Target = Target.TeminalB, FlightId = 3 },
                new Flight { Target = Target.TeminalA, FlightId = 4 },
                new Flight { Target = Target.TeminalA, FlightId = 5 },
                new Flight { Target = Target.TeminalB, FlightId = 6 },
                new Flight { Target = Target.TeminalA, FlightId = 7 },
                new Flight { Target = Target.TeminalB, FlightId = 8 },
                new Flight { Target = Target.TeminalA, FlightId = 9 },
                new Flight { Target = Target.TeminalB, FlightId = 10 },

                new Flight { Target = Target.TeminalA, FlightId = 11 },
                new Flight { Target = Target.TeminalB, FlightId = 12 },
                new Flight { Target = Target.TeminalB, FlightId = 13 },
                new Flight { Target = Target.TeminalA, FlightId = 14 },
                new Flight { Target = Target.TeminalA, FlightId = 15 },
                new Flight { Target = Target.TeminalB, FlightId = 16 },
                new Flight { Target = Target.TeminalA, FlightId = 17 },
                new Flight { Target = Target.TeminalB, FlightId = 18 },
                new Flight { Target = Target.TeminalA, FlightId = 19 },
                new Flight { Target = Target.TeminalB, FlightId = 20 },

                new Flight { Target = Target.TeminalA, FlightId = 21 },
                new Flight { Target = Target.TeminalB, FlightId = 22 },
                new Flight { Target = Target.TeminalB, FlightId = 23 },
                new Flight { Target = Target.TeminalA, FlightId = 24 },
                new Flight { Target = Target.TeminalA, FlightId = 25 },
                new Flight { Target = Target.TeminalB, FlightId = 26 },
                new Flight { Target = Target.TeminalA, FlightId = 27 },
                new Flight { Target = Target.TeminalB, FlightId = 28 },
                new Flight { Target = Target.TeminalA, FlightId = 29 },
                new Flight { Target = Target.TeminalB, FlightId = 30 },

                new Flight { Target = Target.TeminalA, FlightId = 31 },
                new Flight { Target = Target.TeminalB, FlightId = 32 },
                new Flight { Target = Target.TeminalB, FlightId = 33 },
                new Flight { Target = Target.TeminalA, FlightId = 34 },
                new Flight { Target = Target.TeminalA, FlightId = 35 },
                new Flight { Target = Target.TeminalB, FlightId = 36 },
                new Flight { Target = Target.TeminalA, FlightId = 37 },
                new Flight { Target = Target.TeminalB, FlightId = 38 },
                new Flight { Target = Target.TeminalA, FlightId = 39 },
                new Flight { Target = Target.TeminalB, FlightId = 40 },

                new Flight { Target = Target.TeminalA, FlightId = 51 },
                new Flight { Target = Target.TeminalB, FlightId = 52 },
                new Flight { Target = Target.TeminalB, FlightId = 53 },
                new Flight { Target = Target.TeminalA, FlightId = 54 },
                new Flight { Target = Target.TeminalA, FlightId = 55 },
                new Flight { Target = Target.TeminalB, FlightId = 56 },
                new Flight { Target = Target.TeminalA, FlightId = 57 },
                new Flight { Target = Target.TeminalB, FlightId = 58 },
                new Flight { Target = Target.TeminalA, FlightId = 59 },
                new Flight { Target = Target.TeminalB, FlightId = 60 },
            };

            var taskList = flights.Select((f) => airport.AddFlight(f)).ToArray();
            var stopwatch = Stopwatch.StartNew();
            Task.WaitAll(taskList);
            Console.WriteLine($"Total Time: {stopwatch.Elapsed}");
            Console.ReadKey();
        }
    }
}