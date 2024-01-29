using Horizon.Aplication.Dtos;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IClassTypeService
    {
        Task<Result<IEnumerable<ClassTypeDto>>> GetAllClassTypes();

    }
}
