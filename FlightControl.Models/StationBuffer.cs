﻿namespace FlightControl.Models
{
    public class StationBuffer : Station
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        public StationBuffer(int id, string name, Action<object, Flight> onUpdate) : base(id, name, onUpdate) { }

        internal new async Task Enter(Flight flight)
        {
            await semaphore.WaitAsync();
            base.Enter(flight);
        }

        internal override void Exit()
        {
            semaphore.Release();
            base.Exit();
        }
    }
}