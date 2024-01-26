using Horizon.Aplication.Dtos;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetAllCities();
    }
}
