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
            Result<BaggageTagDto> baggageTagDtoResult = await _blogTagService.GenerateBaggageTag(idTicket);
            if (baggageTagDtoResult.Success)
                return Ok(baggageTagDtoResult.Data);
            if (baggageTagDtoResult.StatusCode == 404)
                return NotFound(baggageTagDtoResult);
            return BadRequest(baggageTagDtoResult);
        }
    }
}
