using Horizon.Aplication.Dtos;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IVisitorService
    {
        Task<VisitorDto> GetVisitorById(Guid id);
        Task<VisitorDto> CreateNewVisitor(VisitorDto visitorDto);

    }
}
