using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;
        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<FlightDto> resultDto = await _flightService.GetAllFlights();
            if (resultDto is null) return NotFound("Não existem voos cadastrados");
            return Ok(resultDto);
        }

        [HttpGet("{id:Guid}", Name = "GetFlightById")]
        public async Task<IActionResult> GetFlightById(Guid id)
        {
            FlightDto resultDto = await _flightService.GetFlightById(id);
            if (resultDto is null) return NotFound("Voo não encontrado");
            return Ok(resultDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] FlightDto flightDto)
        {

            if (flightDto is null) return BadRequest("Dados para criação do Voo inválidos");

            FlightDto createdFlight = await _flightService.CreateFlight(flightDto);

            if (createdFlight != null)
                return new CreatedAtRouteResult("GetFlightById", new { id = createdFlight.Id }, createdFlight);
            return BadRequest("Houve um erro ao cadastrar voo");
        }

        [HttpPut("CancelFlight/{id:Guid}")]
        public async Task<IActionResult> CancelFlight(Guid id)
        {
            bool FlightDeleted = await _flightService.CancelFlight(id);
            if (FlightDeleted)
                return Ok("Voo cancelado com sucesso");
            return BadRequest("Erro ao cancelar voo");
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> ModifyFlight(Guid id, [FromBody] FlightDto flightDto)
        {
            if (flightDto is null) return BadRequest("Dados para alteração do Voo inválidos");
            FlightDto resultDto = await _flightService.ModifyFlight(flightDto);
            if (resultDto is not null)
                return Ok(resultDto);
            return BadRequest("Houve um erro ao modificar o voo");

        }

    }
}
