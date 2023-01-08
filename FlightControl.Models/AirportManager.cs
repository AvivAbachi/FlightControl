namespace FlightControl.Models
{
    public class AirportManager : IAirportManager
    {
        public event EventHandler<FlightsArgs> OnFlightUpdate;

        private readonly SemaphoreSlim safeBuffer = new(4);
        private readonly List<Flight> flights = new();
        private readonly IAirportConfig config;

        #region Singelton
        private static AirportManager? instance = null;
        public static AirportManager GetInstance => instance ?? throw new InvalidOperationException("Airport Manager not created, use create method first!");

        private AirportManager()
        {
            throw new InvalidOperationException("Airport Manager not created, use create method first!");
        }
        private AirportManager(IAirportConfig config)
        {
            this.config = config;
            for (int i = 0; i < this.config.Stations.Length; i++)
            {
                this.config.Stations[i].OnUpdate = FlightUpdate;
            }
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
            var path = config.GetPath(flight);
            if (path == null) throw new ArgumentNullException("Target is not vaild", nameof(flight.Target));
            flights.Add(flight);
            flight.Station = new Station { Name = "Wait" };
            await safeBuffer.WaitAsync();
            if (flight.Target == Target.Departure) await flight.EnterToTerminal(config.Terminals);
            await flight.ProseecePlan(path);
            if (flight.Target == Target.Arrival) await flight.EnterToTerminal(config.Terminals);
            safeBuffer.Release();
            flight.Station!.Exit(flight);
            flight.Station = new Station { Name = "Done" };
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
