using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
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
            Result<IEnumerable<FlightDetailsDto>> result = await _flightService.GetAllFlights();
            if (result.Success)
                return Ok(result);
            if (result.StatusCode == 404)
                return NotFound(result);
            return BadRequest(result);
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

            Result<FlightDto> result = await _flightService.CreateFlight(flightDto);
            if (result.Success)
                return new CreatedAtRouteResult("GetFlightById", new { id = result.Data.Id }, result);
            return BadRequest(result);
                
        }

        [HttpPut("CancelFlight/{id:Guid}")]
        public async Task<IActionResult> CancelFlight(Guid id)
        {
            Result<bool> result = await _flightService.CancelFlight(id);
            if (result.Success)
                return Ok(result);
            if (result.StatusCode == 404)
                return NotFound(result);
            return BadRequest(result);
        }

        [HttpPut("{idFlight:Guid}")]
        public async Task<IActionResult> UpdateFlight(Guid idFlight, [FromBody] FlightDto flightDto)
        {
            if (flightDto is null) return BadRequest("Dados para alteração do Voo inválidos");
            Result<FlightDto> result = await _flightService.UpdateFlight(idFlight, flightDto);
            if (result.Success)
                return Ok(result);
            if (result.StatusCode == 404)
                return NotFound(result);
            return BadRequest(result);

        }

    }
}
