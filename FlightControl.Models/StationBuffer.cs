namespace FlightControl.Models
{
    public class StationBuffer : Station
    {
        private readonly SemaphoreSlim semaphore = new(1);
        internal new async Task Enter(Flight flight)
        {
            await semaphore.WaitAsync();
            base.Enter(flight);
        }

        internal override void Exit(Flight flight)
        {
            semaphore.Release();
            base.Exit(flight);
        }
    }
}