using Horizon.Aplication.Dtos;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IVisitorService
    {
        Task<Result<VisitorDto>> CreateNewVisitor(VisitorDto visitorDto);
        Task<Result<VisitorDto>> GetVisitorById(Guid id);

    }
}
