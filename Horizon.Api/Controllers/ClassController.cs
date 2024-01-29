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
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpPut("ChangePriceSeats/{id:Guid}")]
        public async Task<IActionResult> ChangeSeatsPrice(Guid id, [FromBody] ChangeSeatsPriceDto changePriceDto)
        {
            Result<ClassDto> result = await _classService.ChangeSeatsPrice(id, changePriceDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);


        }
        [HttpPost]
        public async Task<IActionResult> CreateClassToVoo(List<ClassDto> classDto)
        {
            if (classDto is null) return BadRequest("Preencha os dados da classe corretamente");
            Result<List<ClassDto>> result = await _classService.CreateClassToFlight(classDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
