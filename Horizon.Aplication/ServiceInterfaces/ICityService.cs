using Horizon.Aplication.Dtos;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface ICityService
    {
        Task<Result<IEnumerable<CityDto>>> GetAllCities();
    }
}
