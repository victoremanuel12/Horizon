using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorService _visitorService;
        public VisitorController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetVisitorById(Guid id)
        {
            Result<VisitorDto> result = await _visitorService.GetVisitorById(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateVisitor(VisitorDto visitorDto)
        {
            Result<VisitorDto> result = await _visitorService.CreateNewVisitor(visitorDto);
            if (result.Success)
                return Ok(result);
            if (result.StatusCode == 404)
                return NotFound(result);
            return BadRequest(result);
        }
    }
}
