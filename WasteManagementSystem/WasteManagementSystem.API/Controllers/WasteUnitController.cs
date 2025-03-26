using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.WasteUnitService;
using WasteManagementSystem.Data.Models;

namespace WasteManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WasteUnitController : BaseController
    {
        private readonly IWasteUnitService _wasteUnitService;
        public WasteUnitController(IWasteUnitService wasteUnitService)
        {
            _wasteUnitService = wasteUnitService;
        }
        [HttpGet("{id}")]
        public async Task<WasteUnit?> GetWasteUnitById(int id)
        {
            var result = await _wasteUnitService.GetWasteUnitById(id);
            return result;
        }
        [HttpGet]
        public async Task<List<WasteUnit>> GetWasteUnit()
        {
            var result =  await _wasteUnitService.GetWasteUnit();
            return result;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddWasteUnit([FromBody] WasteUnitDto wasteType)
        {
            await _wasteUnitService.AddWasteUnit(wasteType);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWasteUnit(int id)
        {
            var result = await _wasteUnitService.DeleteWasteUnit(id);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateWasteUnit([FromBody] WasteUnit wasteUnit)
        {
            var result =  await _wasteUnitService.UpdateWasteUnit(wasteUnit);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
