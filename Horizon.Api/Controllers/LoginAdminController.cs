using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAdminController : ControllerBase
    {
        private readonly ILoginAdminService _loginAdminService;

        public LoginAdminController(ILoginAdminService loginAdminService)
        {
            _loginAdminService = loginAdminService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDto loginDto)
        {
            Result<LoginDto> result = await _loginAdminService.LoginAdmin(loginDto);
            if (result.Success)
                return Ok(result);
            if (result.StatusCode == 404)
                return NotFound(result);
            return BadRequest(result);
        }

    }
}
