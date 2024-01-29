using Horizon.Aplication.Dtos;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IBuyerService
    {
        Task<Result<BuyerDto>> CreateNewBuyer(BuyerDto buyerDto);
        Task<Result<BuyerDto>> GetByerById(Guid id);
    }
}
