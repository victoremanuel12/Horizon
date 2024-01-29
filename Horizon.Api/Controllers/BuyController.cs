using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Aplication.Services;
using Horizon.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyController : ControllerBase
    {
        private readonly IBuyService _buyService;
        public BuyController(IBuyService buyService)
        {
            _buyService = buyService;
        }
        [HttpPost]
        public async Task<IActionResult> OrderBuy(BuyDto buyDto)
        {
            
            Result<BuyDto> result = await _buyService.OrderBuyTikets(buyDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
          
        }
        [HttpPut("CancelBuy/{idBuy:Guid}")]
        public async Task<IActionResult> CancelBuy(Guid idBuy)
        {
            Result<BuyDto> result = await _buyService.CancelBuy(idBuy);
            if (result.Success)
                return Ok(result);
            if (result.StatusCode == 404)
                return NotFound(result);
            return BadRequest(result);
        }
    }
}
