using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GenereteVoucher")]
        public async Task<IActionResult> GenereteVoucher(Guid IdTicket)
        {
            VoucherDto vouherDtoResult = await _voucherService.GenereteVoucher(IdTicket);
            if (vouherDtoResult is null)
                return BadRequest("Erro ao emitir Voucher");
            return Ok(vouherDtoResult);
        }
    }
}
