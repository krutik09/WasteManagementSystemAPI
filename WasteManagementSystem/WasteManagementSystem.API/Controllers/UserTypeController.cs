using Microsoft.AspNetCore.Mvc;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.UserService;

namespace WasteManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService _userTypeService;
        public UserTypeController(IUserTypeService userTypeService)
        {

            _userTypeService = userTypeService;
        }
        [HttpPost]
        public async Task<IActionResult> AddUserType([FromBody] UserTypeDto userType)
        {
            await _userTypeService.AddAsync(userType);
            return Ok();
        }
        [HttpGet]
        public async Task<List<UserTypeDto>> GetUserType()
        {
            var result = await _userTypeService.GetAllAsync();
            return result;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUserType([FromBody] UserTypeDto userType)
        {
            await _userTypeService.DeleteAsync(userType);
            return Ok();
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUserType([FromBody] UserTypeDto userType)
        {
            await _userTypeService.UpdateAsync(userType);
            return Ok();
        }
    }
}
