using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Aplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassTypeController : ControllerBase
    {
        private readonly IClassTypeService _classTypeService;

        public ClassTypeController(IClassTypeService classTypeService)
        {
            _classTypeService = classTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<ClassTypeDto> resultDto = await _classTypeService.GetAllClassTypes();
            if (resultDto is null) return NotFound("Não existem tipo de classes cadastradas");
            return Ok(resultDto);
        }
    }
}
