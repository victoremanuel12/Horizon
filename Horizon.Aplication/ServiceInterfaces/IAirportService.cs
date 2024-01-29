using Horizon.Aplication.Dtos;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IAirportService
    {
        Task<Result<IEnumerable<AirportDto>>> GetAllFlights();
    }
}
