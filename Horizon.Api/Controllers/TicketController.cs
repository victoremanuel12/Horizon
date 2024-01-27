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
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string cpf)
        {
            if (cpf is null) return BadRequest("CPF inválido");
            IEnumerable<TicketDto> ticketsDtoResult = await _ticketService.GetTicketByCpf(cpf);
            if (ticketsDtoResult.Any())
                return Ok(ticketsDtoResult);
            return NotFound("Nenhuma passagem foi encontrada para esse CPF");
        }
        [HttpPost]
        public async Task<IActionResult> BuyTicket([FromBody] List<TicketDto> ticketsDto)
        {
            if (ticketsDto is null) return BadRequest("Dados para a compra inválidos");
            List<TicketDto> ticketsDtoResult = await _ticketService.BuyTicket(ticketsDto);
            if (ticketsDtoResult is not null)
                return Ok(ticketsDtoResult);
            return BadRequest("Não existe acentos para essa classe");
        }
    }
}
