using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [HttpGet()]
        public async Task<IActionResult> List()
        {
            Result<IEnumerable<CityDto>> result = await _cityService.GetAllCities();
            if (result.Success)
                return Ok(result);
            if (result.StatusCode == 404)
                return NotFound(result);
            return BadRequest(result);
        }
    }
}
