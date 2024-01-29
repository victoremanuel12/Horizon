using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [HttpGet("GenerateVoucher")]
        public async Task<IActionResult> GenerateVoucher(Guid IdTicket)
        {
            Result<VoucherDto> result = await _voucherService.GenerateVoucher(IdTicket);

            if (result.Success)
                return Ok(result);
            if(result.StatusCode == 404)
                return NotFound(result);
            return BadRequest(result);

        }
    }
}
