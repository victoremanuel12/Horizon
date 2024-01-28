using Horizon.Aplication.Dtos;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IBuyerService
    {
        Task<BuyerDto> GetByerById(Guid id);
        Task<BuyerDto> CreateNewBuyer(BuyerDto buyerDto);
    }
}
