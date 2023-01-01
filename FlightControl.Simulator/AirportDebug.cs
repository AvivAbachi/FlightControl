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
                Console.WriteLine($"{e.flight.Id}\t{(o as Station)?.Name}");
            };

            var flights = new Flight[]
            {
                new Flight { Target = Target.TerminalA, Id = 1 },
                new Flight { Target = Target.TerminalB, Id = 2 },
                new Flight { Target = Target.TerminalB, Id = 3 },
                new Flight { Target = Target.TerminalA, Id = 4 },
                new Flight { Target = Target.TerminalA, Id = 5 },
                new Flight { Target = Target.TerminalB, Id = 6 },
                new Flight { Target = Target.TerminalA, Id = 7 },
                new Flight { Target = Target.TerminalB, Id = 8 },
                new Flight { Target = Target.TerminalA, Id = 9 },
                new Flight { Target = Target.TerminalB, Id = 10 },

                new Flight { Target = Target.TerminalA, Id = 11 },
                new Flight { Target = Target.TerminalB, Id = 12 },
                new Flight { Target = Target.TerminalB, Id = 13 },
                new Flight { Target = Target.TerminalA, Id = 14 },
                new Flight { Target = Target.TerminalA, Id = 15 },
                new Flight { Target = Target.TerminalB, Id = 16 },
                new Flight { Target = Target.TerminalA, Id = 17 },
                new Flight { Target = Target.TerminalB, Id = 18 },
                new Flight { Target = Target.TerminalA, Id = 19 },
                new Flight { Target = Target.TerminalB, Id = 20 },

                new Flight { Target = Target.TerminalA, Id = 21 },
                new Flight { Target = Target.TerminalB, Id = 22 },
                new Flight { Target = Target.TerminalB, Id = 23 },
                new Flight { Target = Target.TerminalA, Id = 24 },
                new Flight { Target = Target.TerminalA, Id = 25 },
                new Flight { Target = Target.TerminalB, Id = 26 },
                new Flight { Target = Target.TerminalA, Id = 27 },
                new Flight { Target = Target.TerminalB, Id = 28 },
                new Flight { Target = Target.TerminalA, Id = 29 },
                new Flight { Target = Target.TerminalB, Id = 30 },

                new Flight { Target = Target.TerminalA, Id = 31 },
                new Flight { Target = Target.TerminalB, Id = 32 },
                new Flight { Target = Target.TerminalB, Id = 33 },
                new Flight { Target = Target.TerminalA, Id = 34 },
                new Flight { Target = Target.TerminalA, Id = 35 },
                new Flight { Target = Target.TerminalB, Id = 36 },
                new Flight { Target = Target.TerminalA, Id = 37 },
                new Flight { Target = Target.TerminalB, Id = 38 },
                new Flight { Target = Target.TerminalA, Id = 39 },
                new Flight { Target = Target.TerminalB, Id = 40 },

                new Flight { Target = Target.TerminalA, Id = 51 },
                new Flight { Target = Target.TerminalB, Id = 52 },
                new Flight { Target = Target.TerminalB, Id = 53 },
                new Flight { Target = Target.TerminalA, Id = 54 },
                new Flight { Target = Target.TerminalA, Id = 55 },
                new Flight { Target = Target.TerminalB, Id = 56 },
                new Flight { Target = Target.TerminalA, Id = 57 },
                new Flight { Target = Target.TerminalB, Id = 58 },
                new Flight { Target = Target.TerminalA, Id = 59 },
                new Flight { Target = Target.TerminalB, Id = 60 },
            };

            var taskList = flights.Select((f) => airport.AddFlight(f)).ToArray();
            var stopwatch = Stopwatch.StartNew();
            Task.WaitAll(taskList);
            Console.WriteLine($"Total Time: {stopwatch.Elapsed}");
            Console.ReadKey();
        }
    }
}