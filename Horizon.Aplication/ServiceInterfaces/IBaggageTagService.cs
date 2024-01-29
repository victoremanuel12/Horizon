using Horizon.Aplication.Dtos;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface IBaggageTagService
    {
        Task<Result<BaggageTagDto>> GenerateBaggageTag(Guid idTicket);
    }
}
