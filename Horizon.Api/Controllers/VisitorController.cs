using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

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
            VisitorDto visitorDtoResult = await _visitorService.GetVisitorById(id);
            if(visitorDtoResult is null) 
                return NotFound("Visitante não encontrado");
            return Ok(visitorDtoResult);
        }
        [HttpPost]
        public async Task<IActionResult> CreateVisitor(VisitorDto visitorDto)
        {
            VisitorDto visitorDtoResult = await _visitorService.CreateNewVisitor(visitorDto);
            if (visitorDtoResult is null)
                return BadRequest("Erro ao cadastrar visitante");
            return Ok(visitorDtoResult);
        }
    }
}
