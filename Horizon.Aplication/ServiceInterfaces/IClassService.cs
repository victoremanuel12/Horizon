using Horizon.Aplication.Dtos;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IClassService
    {
        Task<ClassDto> ChangeSeatsPrice(Guid classId, ClassDto classDto);
        Task<List<ClassDto>> CreateClassToFlight(List<ClassDto> classes);

    }
}
