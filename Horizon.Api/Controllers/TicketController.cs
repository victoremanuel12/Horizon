using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using static Horizon.Domain.Validation.ErroResultOperation;

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
        [HttpGet("GetFlightByCpf")]
        public async Task<ActionResult> Get([FromQuery] string cpf)
        {
            if (cpf is null) return BadRequest("CPF inválido");
            Result<IEnumerable<TicketDto>> result = await _ticketService.GetTicketByCpf(cpf);
            if (result.Success)
                return Ok(result);
            if (result.StatusCode == 404)
                return NotFound(result);
            return BadRequest(result);
        }
        [HttpPost]
        public async Task<IActionResult> BuyTicket([FromBody] List<TicketDto> ticketsDto)
        {
            if (ticketsDto is null) return BadRequest("Dados para a compra inválidos");
            Result<List<TicketDto>> result = await _ticketService.BuyTickets(ticketsDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
