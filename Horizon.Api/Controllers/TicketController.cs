using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpPost]
        public async Task<IActionResult> BuyTicket([FromBody] TicketDto ticketDto)
        {
            if (ticketDto is null) return BadRequest("Dados para a compra inválidos");
            TicketDto ticketDtoResult = await  _ticketService.BuyTicket(ticketDto);
            if (ticketDtoResult is not null)
                return new CreatedAtRouteResult(new { id = ticketDtoResult.Id }, ticketDtoResult);
            return BadRequest("Houve um erro ao cadastrar voo");
        }
    }
}
