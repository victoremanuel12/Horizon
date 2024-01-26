using Horizon.Aplication.Dtos;

namespace Horizon.Aplication.ServiceInterfaces
{
    public interface ITicketService
    {
        Task<TicketDto> BuyTicket(TicketDto ticketDto);
       
    }
}
