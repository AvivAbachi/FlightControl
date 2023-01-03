using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace FlightControl.Models
{
    public class Flight
    {
        static int seed = Environment.TickCount;
        static readonly ThreadLocal<Random> random = new(() => new Random(Interlocked.Increment(ref seed)));

        [Key]
        public int FlightId { get; set; }
        public Target Target { get; set; }
        public string? Airline { get; set; }
        public string? ComeingForm { get; set; }
        public string? DepartingTo { get; set; }
        [NotMapped]
        public Point Location { get; set; } = new Point { X=1000,Y=50};
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        [ForeignKey("stationId")]
        public virtual Station? Station { get; set; }

        internal async Task ProseecePlan(Station[] path, SemaphoreSlim safeBuffer)
        {
            await Task.Run(async () =>
           {
               path[0].Enter(this);
               //Location.X = (path[0].Start.X + path[0].End.X) / 2;
               //Location.Y = (path[0].Start.Y + path[0].End.Y) / 2;
               await safeBuffer.WaitAsync();
               ArrivalDate = DateTime.Now;
               for (int i = 1; i < path.Length - 1; i++)
               {
                   if (i == 3) safeBuffer.Release();
                   await ((StationBuffer)path[i]).Enter(this);
                   Location.X = path[i].Start.X;
                   Location.Y = path[i].Start.Y;

                   while (!Location.Equals(Station!.End))
                   {
                       Location.Step(Station.End);
                       await Task.Delay(100);
                   }
                   if (Station.StationId == 6 || Station.StationId == 8)
                   {
                       await Task.Delay(random.Value.Next(5000, 15000));
                       while (!Location.Equals(Station!.Start))
                       {
                           Location.Step(Station.Start);
                           await Task.Delay(100);
                       }
                   }
               }
               path[^1].Enter(this);
               DepartureDate = DateTime.Now;
               //Location.X = (path[^1].Start.X + path[^1].End.X) / 2;
               //Location.Y = (path[^1].Start.Y + path[^1].End.Y) / 2;
               Station?.Exit(this);
           });
        }
    }
}