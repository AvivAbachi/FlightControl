using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FlightControl.Models
{
    public class Flight
    {
        static readonly int STEP_SPEED = 10;
        static readonly int STEP_DELAY = 100;

        static int seed = Environment.TickCount;
        static readonly ThreadLocal<Random> random = new(() => new Random(Interlocked.Increment(ref seed)));

        [Key]
        public int FlightId { get; set; }
        public Target Target { get; set; }
        public string? Airline { get; set; }
        public string? ComeingForm { get; set; }
        public string? DepartingTo { get; set; }
        [NotMapped]
        public Point Location { get; set; } = new Point { X = 800, Y = 50 };
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        [ForeignKey("stationId")]
        public virtual Station? Station { get; set; }

        internal async Task ProseecePlan(Station[] path, SemaphoreSlim safeBuffer)
        {
            await Task.Run(async () =>
           {
               path[0].Enter(this);
               await safeBuffer.WaitAsync();
               ArrivalDate = DateTime.Now;
               for (int i = 1; i < path.Length - 1; i++)
               {
                   await ((StationBuffer)path[i]).Enter(this);
                   if (i == 3) safeBuffer.Release();
                   await Location.Step(Station!.Start, STEP_SPEED, STEP_DELAY);
                   await Location.Step(Station.End, STEP_SPEED, STEP_DELAY);
                   if (Station.StationId == 6 || Station.StationId == 8)
                   {
                       await Task.Delay(random.Value!.Next(5000, 15000));
                       await Location.Step(Station.Start, STEP_SPEED, STEP_DELAY);
                   }
               }
               DepartureDate = DateTime.Now;
               path[^1].Enter(this);
               Station?.Exit(this);
           });
        }
    }
}