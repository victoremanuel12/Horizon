using Horizon.Aplication.Dtos;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IClassTypeService
    {
        Task<IEnumerable<ClassTypeDto>> GetAllClassTypes();

    }
}
