using Horizon.Aplication.Dtos;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IBuyService
    {
        Task<Result<BuyDto>> OrderBuyTikets(BuyDto buyDto);
        Task<Result<BuyDto>> CancelBuy(Guid idBuy);

    }
}
