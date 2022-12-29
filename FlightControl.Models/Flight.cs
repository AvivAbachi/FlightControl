namespace FlightControl.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public Target Target { get; set; }
        public virtual Station? Station { get; set; }
        internal async Task ProseecePlan(Station[] path, SemaphoreSlim safeBuffer)
        {
            await Task.Run(async () =>
           {
               path[0].Enter(this);
               await safeBuffer.WaitAsync();
               for (int i = 1; i < path.Length - 1; i++)
               {
                   if (i == 3) safeBuffer.Release();
                   await ((StationBuffer)path[i]).Enter(this);
                   await Task.Delay(100);
               }
               path[path.Length - 1].Enter(this);
               Station?.Exit();
           });
        }
    }
}