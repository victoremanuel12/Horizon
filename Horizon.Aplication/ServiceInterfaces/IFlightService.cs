using Horizon.Aplication.Dtos;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightDto>> GetAllFlights();

        Task<FlightDto> GetFlightById(Guid flightId);

        Task<FlightDto> CreateFlight(FlightDto flightDto);

        Task UpdateFlight(Guid flightId, FlightDto flightDto);

        Task<FlightDto> ModifyFlight(FlightDto flightDto);
        Task<bool> CancelFlight(Guid flightId);


    }
}
