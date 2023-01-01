using System.ComponentModel.DataAnnotations.Schema;

namespace FlightControl.Models
{
    public class Flight
    {
        static int seed = Environment.TickCount;
        static readonly ThreadLocal<Random> random = new(() => new Random(Interlocked.Increment(ref seed)));

        public int Id { get; set; }
        public Target Target { get; set; }
        public string? Airline { get; set; }
        public string? ComeingForm { get; set; }
        public string? DepartingTo { get; set; }
        [NotMapped]
        public Point Location { get; set; } = new Point();
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
                   if (i == 3) safeBuffer.Release();
                   await ((StationBuffer)path[i]).Enter(this);
                   Location.X = (path[i].Start.X + path[i].End.X) / 2;
                   Location.Y = (path[i].Start.Y + path[i].End.Y) / 2;
                   await Task.Delay(random.Value?.Next(1000, 5000) ?? 1000);
               }
               DepartureDate = DateTime.Now;
               path[^1].Enter(this);
               Location.X = (path[^1].Start.X + path[^1].End.X) / 2;
               Location.Y = (path[^1].Start.Y + path[^1].End.Y) / 2;
               Station?.Exit();
           });
        }
    }
}