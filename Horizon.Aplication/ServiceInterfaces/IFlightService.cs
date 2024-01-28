using Horizon.Aplication.Dtos;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IFlightService
    {
        Task<List<FlightWithNameAirportDto>> GetAllFlights();

        Task<FlightWithNameAirportDto> GetFlightById(Guid flightId);

        Task<FlightDto> CreateFlight(FlightDto flightDto);

        Task UpdateFlight(Guid flightId, FlightDto flightDto);

        Task<FlightDto> ModifyFlight(FlightDto flightDto);
        Task<bool> CancelFlight(Guid flightId);


    }
}
