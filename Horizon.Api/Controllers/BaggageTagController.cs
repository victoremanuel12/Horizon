using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaggageTagController : ControllerBase
    {
        private readonly IBaggageTagService _blogTagService;

        public BaggageTagController(IBaggageTagService blogTagService)
        {
            _blogTagService = blogTagService;
        }

        [HttpGet("{idTicket:Guid}")]
        public async Task<IActionResult> GenereteBaggageTag(Guid idTicket)
        {
            Result<BaggageTagDto> result = await _blogTagService.GenerateBaggageTag(idTicket);
            if (result.Success)
                return Ok(result);
            if (result.StatusCode == 404)
                return NotFound(result);
            return BadRequest(result);
        }
    }
}
