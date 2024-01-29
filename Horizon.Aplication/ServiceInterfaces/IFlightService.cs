using Horizon.Aplication.Dtos;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IFlightService
    {
        Task<List<FlightDetailsDto>> GetAllFlights();

        Task<Result<FlightDetailsDto>> GetFlightById(Guid flightId);

        Task<FlightDto> CreateFlight(FlightDto flightDto);

        Task<FlightDto> UpdateFlight(Guid id,FlightDto flightDto);
        Task<bool> CancelFlight(Guid flightId);


    }
}
