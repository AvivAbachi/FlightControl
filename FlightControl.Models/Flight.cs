using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FlightControl.Models
{
    public class Flight
    {
        static int seed = Environment.TickCount;
        static readonly ThreadLocal<Random> random = new(() => new Random(Interlocked.Increment(ref seed)));

        [Key]
        public int FlightId { get; set; }
        [ForeignKey("stationId")]
        public virtual Station? Station { get; set; }
        public string? Airline { get; set; }
        public string? Airport { get; set; }
        public DateTime? LandingDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? BoardingDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public Target Target { get; set; }
        [NotMapped]
        public Point Location { get; set; } = new();

        internal async Task ProseecePlan(Station[] path, Station[] terminals)
        {
            await Task.Run(async () =>
           {
               if (Target == Target.Departure) await EnterToTerminal(terminals);
               foreach (Station station in path)
               {
                   if (station.StationId == 5) LandingDate = DateTime.Now;
                   await station.EnterAsync(this);
                   await Task.Delay(random.Value!.Next(1000,5000));
               }
               if (Target == Target.Arrival) await EnterToTerminal(terminals);
               if (Target == Target.Departure) DepartureDate = DateTime.Now;
           });
        }

        private async Task EnterToTerminal(Station[] terminals)
        {
            var token = new CancellationTokenSource();
            await Task.WhenAny(terminals.Select(async (t, i) => await t.EnterAsync(this, token.Token)));
            token.Cancel();
            if (Target == Target.Departure) BoardingDate = DateTime.Now;
            if (Target == Target.Arrival) ArrivalDate = DateTime.Now;
            await Task.Delay(random.Value!.Next(5000, 7500));
        }
    }
}