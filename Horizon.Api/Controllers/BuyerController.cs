using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Aplication.Services;
using Microsoft.AspNetCore.Mvc;

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
            BuyerDto buyerDtoResult = await _buyerService.GetByerById(id);
            if(buyerDtoResult == null)
                return NotFound("Comprador não encontrado");
            return Ok(buyerDtoResult);
        }
        [HttpPost]
        public async Task<IActionResult> CreateVisitor(BuyerDto buyerDto)
        {
            BuyerDto buyerDtoResult = await _buyerService.CreateNewBuyer(buyerDto);
            if (buyerDtoResult is null)
                return BadRequest("Erro ao cadastrar visitante");
            return Ok(buyerDtoResult);
        }
    }
}
