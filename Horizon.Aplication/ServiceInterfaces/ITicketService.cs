using Horizon.Aplication.Dtos;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface ITicketService
    {
        Task<List<TicketDto>> BuyTicket(List<TicketDto> ticketDto);
        Task<IEnumerable<TicketDto>> GetTicketByCpf(string cpf);
       
    }
}
