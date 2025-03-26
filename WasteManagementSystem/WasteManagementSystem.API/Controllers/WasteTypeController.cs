using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.WasteTypeService;
using WasteManagementSystem.Data.Models;

namespace WasteManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WasteTypeController : BaseController
    {
        private readonly IWasteTypeService _wasteTypeService;
        public WasteTypeController(IWasteTypeService wasteTypeService)
        {
            _wasteTypeService = wasteTypeService;
        }
        [HttpGet("{id}")]
        public async Task<WasteType?> GetWasteTypeById(int id)
        {
            var result = await _wasteTypeService.GetWasteTypeById(id);
            return result;
        }
        [HttpGet]
        public async Task<List<WasteType>> GetWasteType()
        {
            var result = await _wasteTypeService.GetWasteType();
            return result;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddWasteType([FromBody] WasteTypeDto wasteType)
        {
            await _wasteTypeService.AddWasteUnit(wasteType);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWasteType(int id)
        {
            var result = await _wasteTypeService.DeleteWasteUnit(id);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateWasteType([FromBody] WasteType wasteType)
        {
            var result = await _wasteTypeService.UpdateWasteUnit(wasteType);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
