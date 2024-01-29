using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Aplication.Services;
using Horizon.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using static Horizon.Domain.Validation.ErroResultOperation;

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

        [HttpGet("AllFlights")]
        public async Task<IActionResult> List()
        {
            List<FlightDetailsDto> resultDto = await _flightService.GetAllFlights();
            if (!resultDto.Any())
                return NotFound("Não existem voos cadastrados");
            return Ok(resultDto);
        }

        [HttpGet("{id:Guid}", Name = "GetFlightById")]
        public async Task<IActionResult> GetFlightById(Guid id)
        {
            Result<FlightDetailsDto> result = await _flightService.GetFlightById(id);
            if (result.Success)
                return Ok(result);
            if (result.StatusCode == 404)
                return NotFound(result);
            return BadRequest(result);

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

        [HttpPut("{idFlight:Guid}")]
        public async Task<IActionResult> UpdateFlight(Guid idFlight, [FromBody] FlightDto flightDto)
        {
            if (flightDto is null) return BadRequest("Dados para alteração do Voo inválidos");
            FlightDto resultDto = await _flightService.UpdateFlight(idFlight, flightDto);
            if (resultDto is not null)
                return Ok(resultDto);
            return BadRequest("Houve um erro ao modificar o voo");

        }

    }
}
