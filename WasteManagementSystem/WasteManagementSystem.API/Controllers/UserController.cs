using Microsoft.AspNetCore.Mvc;
using WasteManagementSystem.Business.Auth;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.UserService;

namespace WasteManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;
        public UserController(IUserService userService,ILoginService loginService)
        {
            _userService = userService;
            _loginService = loginService;
        }
        [HttpGet("{id}")]
        public async Task<UserDto?> GetUserById(int id)
        {
            var result = await _userService.GetUserById(id);
            return result;
        }
        [HttpGet]
        public async Task<List<UserDto>> GetUser()
        {
            var result = await _userService.GetUser();
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserRequestDto userDto)
        {
            if((await _loginService.IsEmailExist(userDto.Email)))
            {
                return BadRequest("Email id already exist");
            }
            await _userService.AddUser(userDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
           var result =  await _userService.DeleteUser(id);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost("Update/{id}")]
        public async Task<IActionResult> UpdateUser(int id,[FromBody] UserRequestDto user)
        {
            var result = await _userService.UpdateUser(id,user);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
