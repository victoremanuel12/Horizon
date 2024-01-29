using Horizon.Aplication.Dtos;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IClassService
    {
        Task<Result<ClassDto>> ChangeSeatsPrice(Guid classId, ChangeSeatsPriceDto changePriceDto);
        Task<Result<List<ClassDto>>> CreateClassToFlight(List<ClassDto> classes);

    }
}
