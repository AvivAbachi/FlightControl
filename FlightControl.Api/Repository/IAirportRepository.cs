using FlightControl.Api.Data;
using FlightControl.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightControl.Api.Repository
{
    public interface IAirportRepository
    {
        void AddFlight(Flight flight);
        Flight[] GetAllFlight();
        void RemoveFlight(Flight flight);
        void UpdateFlight( Flight flight);
    }
}