using Microsoft.AspNetCore.Mvc;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.UserService;
using WasteManagementSystem.Data.Models;

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
        [HttpGet("{id}")]
        public async Task<UserType?> GetUserTypeById(int id)
        {
            var result = await _userTypeService.GetUserTypeById(id);
            return result;
        }
        [HttpGet]
        public async Task<List<UserType>> GetUserType()
        {
            var result = await _userTypeService.GetUserType();
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> AddUserType([FromBody] UserTypeDto userType)
        {
            await _userTypeService.AddWasteUnit(userType);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserType(int id)
        {
            var result = await _userTypeService.DeleteWasteUnit(id);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateUserType([FromBody] UserType userType)
        {
            var result = await _userTypeService.UpdateWasteUnit(userType);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
