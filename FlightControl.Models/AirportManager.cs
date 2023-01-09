namespace FlightControl.Models
{
    public class AirportManager : IAirportManager
    {
        public event EventHandler<FlightsArgs> OnFlightUpdate;

        private readonly SemaphoreSlim safeBuffer = new(4);
        private readonly List<Flight> flights = new();
        private readonly IAirportConfig config;

        private static Station wait;
        private static Station done;
        #region Singelton
        private static AirportManager? instance = null;
        public static AirportManager GetInstance => instance ?? throw new InvalidOperationException("Airport Manager not created, use create method first!");

        private AirportManager()
        {
            throw new InvalidOperationException("Airport Manager not created, use create method first!");
        }
        private AirportManager(IAirportConfig config)
        {
            if (instance != null) throw new InvalidOperationException("Airport Manager was created, use GetInstance");
            this.config = config;
            for (int i = 0; i < this.config.Stations.Length; i++)
            {
                this.config.Stations[i].OnUpdate = FlightUpdate;
            }
            wait = new() { Name = "Wait", OnUpdate = FlightUpdate };
            done = new() { Name = "Done", OnUpdate = FlightUpdate };
        }

        public static AirportManager Create(IAirportConfig config)
        {
            if (instance != null) throw new InvalidOperationException("Airport Manager was created, use GetInstance");
            instance = new AirportManager(config);
            return GetInstance;
        }
        #endregion

        public async Task AddFlight(Flight flight)
        {
            var path = config.GetPath(flight) ?? throw new ArgumentNullException("Target is not vaild", nameof(flight.Target));
            flights.Add(flight);
            wait.Enter(flight);
            await safeBuffer.WaitAsync();
            await flight.ProseecePlan(path, config.Terminals);
            done.Enter(flight);
            safeBuffer.Release();
            //await Task.Delay(1000);
            //flights.Remove(flight);
        }

        public IEnumerable<Station> GetAllStations()
        {
            return config.Stations;
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return flights;
        }

        private void FlightUpdate(Flight flight)
        {
            OnFlightUpdate.DynamicInvoke(this, new FlightsArgs(flight));
        }
    }
}
