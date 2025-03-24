using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WasteManagementSystem.Business.Auth;
using WasteManagementSystem.Business.DTO;

namespace WasteManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginDto loginDto)
        {
            var token = await _loginService.Auth(loginDto);
            if (token == null) {
                return BadRequest();
            }
            return Ok(token);
        }
    }
}
