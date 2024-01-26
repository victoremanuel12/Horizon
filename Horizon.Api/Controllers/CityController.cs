using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<CityDto> resultDto = await _cityService.GetAllCities();
            if (resultDto is null) return NotFound("Não existem cidades cadastradas");
            return Ok(resultDto);
        }
    }
}
