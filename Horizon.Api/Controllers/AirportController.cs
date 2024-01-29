using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airplaneService;
        public AirportController(IAirportService airplaneService)
        {
            _airplaneService = airplaneService;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            Result<IEnumerable<AirportDto>> result = await _airplaneService.GetAllFlights();
            if (result.Success)
                return Ok(result);
            if (result.StatusCode == 404)
                return NotFound(result);
            return BadRequest(result);
        }
    }
}
