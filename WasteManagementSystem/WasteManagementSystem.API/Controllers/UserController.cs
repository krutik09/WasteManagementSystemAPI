using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.UserService;

namespace WasteManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUser()
        {
            var result = await _userService.GetAllUser();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            await _userService.AddUser(userDto);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] UserDto userDto)
        {
            await _userService.DeleteAsync(userDto);
            return Ok();
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto user)
        {
            await _userService.UpdateAsync(user);
            return Ok();
        }
    }
}
