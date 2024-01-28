using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

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
            
            BuyDto OrderBuyDtoResult = await _buyService.OrderBuyTikets(buyDto);
            if (OrderBuyDtoResult is null)
                return BadRequest("Houve um erro ao Realizar compra");
            return Ok(OrderBuyDtoResult);
        }
        [HttpPut("CancelBuy")]
        public async Task<IActionResult> CancelBuy(Guid idBuy)
        {
            BuyDto OrderBuyDtoResult = await _buyService.CancelBuy(idBuy);
            if (OrderBuyDtoResult is null)
                return NotFound("Houve um erro ao cancelar a compra");
            return Ok("Compra cancelada com sucesso!");
        }
    }
}
