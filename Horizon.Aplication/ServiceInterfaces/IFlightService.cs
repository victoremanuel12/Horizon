using Horizon.Aplication.Dtos;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IFlightService
    {
        Task<Result<IEnumerable<FlightDetailsDto>>> GetAllFlights();

        Task<Result<FlightDetailsDto>> GetFlightById(Guid flightId);

        Task<Result<FlightDto>> CreateFlight(FlightDto flightDto);

        Task<Result<bool>> CancelFlight(Guid flightId);

        Task<Result<FlightDto>> UpdateFlight(Guid id, FlightDto flightDto);


    }
}
