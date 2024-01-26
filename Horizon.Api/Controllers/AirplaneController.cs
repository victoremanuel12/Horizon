using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirplaneController : ControllerBase
    {
        private readonly IAirplaneService _airplaneService;
        public AirplaneController( IAirplaneService airplaneService)
        {
            _airplaneService = airplaneService;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<AirportDto> resultDto  = await _airplaneService.GetAllFlights();
            if(resultDto is null ) return NotFound("Não existem aeroportos cadastrados");
            return Ok(resultDto);
        }
    }
}
