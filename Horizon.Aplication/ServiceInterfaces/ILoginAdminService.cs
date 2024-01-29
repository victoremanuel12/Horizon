using Horizon.Aplication.Dtos;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface ILoginAdminService
    {
        Task<Result<LoginDto>> LoginAdmin(LoginDto adminData);
    }
}
