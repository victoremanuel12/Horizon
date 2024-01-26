using Horizon.Aplication.Dtos;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IClassService
    {
        Task<ClassDto> ChangeSeatsPrice(Guid classId, ClassDto classDto);
      

    }
}
