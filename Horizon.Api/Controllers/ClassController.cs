using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpPut("ChangePriceSeats/{id:Guid}")]
        public async Task<IActionResult> ChangeSeatsPrice(Guid id, [FromBody] ClassDto classDto)
        {
            if (classDto is null) return BadRequest("Preencha os dados da classe corretamente");
            ClassDto classDtoResult = await _classService.ChangeSeatsPrice(id, classDto);
            if (classDtoResult is null) return BadRequest("Erro ao modificar dados da classe do voo");
            return Ok(classDtoResult);
        }
        
    }
}
