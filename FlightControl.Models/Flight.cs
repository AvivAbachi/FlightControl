using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FlightControl.Models
{
    public class Flight
    {
        //static readonly int STEP_SPEED = 10;
        //static readonly int STEP_DELAY = 100;

        //static int seed = Environment.TickCount;
        //static readonly ThreadLocal<Random> random = new(() => new Random(Interlocked.Increment(ref seed)));

        [Key]
        public int FlightId { get; set; }
        public Target Target { get; set; }
        public string? Airline { get; set; }
        public string? Airport { get; set; }
        [NotMapped]
        public Point? Location { get; set; }// = new Point { X = 800, Y = 50 };
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        [ForeignKey("stationId")]
        public virtual Station? Station { get; set; }

        internal async Task ProseecePlan(Station[] path/*, SemaphoreSlim safeBuffer*/)
        {
            await Task.Run(async () =>
           {
               //ArrivalDate = DateTime.Now;
               for (int i = 0; i < path.Length; i++)
               {
                   //if (path[i].StationId == 3 || path[i].StationId == 8) await safeBuffer.WaitAsync();
                   //if (path[i].StationId == 5 || path[i].StationId == 9) safeBuffer.Release();
                   await path[i].Enter(this);
                   await Task.Delay(1000);
               }
               //DepartureDate = DateTime.Now;
           });
        }

        internal async Task EnterToTerminal(Station[] terminals)
        {
            var token = new CancellationTokenSource();
            await Task.WhenAny(terminals.Select(async (t, i) => await t.Enter(this, token.Token)));
            token.Cancel();
            await Task.Delay(2000);
        }
    }
}
//Location.X = (Station.Start.X + Station.End.X) / 2;
//Location.Y = (Station.Start.Y + Station.End.Y) / 2;