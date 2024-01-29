using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerService _buyerService;

        public BuyerController(IBuyerService buyerService)
        {
            _buyerService = buyerService;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBuyId(Guid id)
        {
            Result<BuyerDto> result = await _buyerService.GetByerById(id);
            if (result.Success)
                return Ok(result);
            return NotFound(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBuyer(BuyerDto buyerDto)
        {

            Result<BuyerDto> result = await _buyerService.CreateNewBuyer(buyerDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);

        }
    }
}
