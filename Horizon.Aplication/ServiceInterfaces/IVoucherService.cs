using Horizon.Aplication.Dtos;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IVoucherService
    {
        Task<Result<VoucherDto>> GenerateVoucher(Guid IdTicket);
    }
}
