using Horizon.Aplication.Dtos;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface ITicketService
    {
        Task<Result<List<TicketDto>>> BuyTickets(List<TicketDto> ticketDtoList);
        Task<Result<IEnumerable<TicketDto>>> GetTicketByCpf(string cpf);

    }
}
