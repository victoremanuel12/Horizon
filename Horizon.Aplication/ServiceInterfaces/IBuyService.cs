using Horizon.Aplication.Dtos;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IBuyService
    {
        Task<BuyDto> OrderBuyTikets(BuyDto buyDto);
        Task<BuyDto> CancelBuy(Guid idBuy);

    }
}
