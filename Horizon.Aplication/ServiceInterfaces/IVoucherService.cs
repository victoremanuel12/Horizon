using Horizon.Aplication.Dtos;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IVoucherService
    {
        Task<VoucherDto> GenereteVoucher(Guid IdTicket);
    }
}
