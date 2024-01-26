using Horizon.Aplication.Dtos;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IAirplaneService
    {
        Task<IEnumerable<AirportDto>> GetAllFlights();
    }
}
