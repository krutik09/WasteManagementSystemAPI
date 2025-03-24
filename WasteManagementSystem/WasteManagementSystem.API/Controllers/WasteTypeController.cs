using Microsoft.AspNetCore.Mvc;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.WasteTypeService;

namespace WasteManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteTypeController : ControllerBase
    {
        private readonly IWasteTypeService _wasteTypeService;
        public WasteTypeController(IWasteTypeService wasteTypeService)
        {
            _wasteTypeService = wasteTypeService;
        }
        [HttpPost]
        public async Task<IActionResult> AddWasteType([FromBody] WasteTypeDto wasteType)
        {
            await _wasteTypeService.AddAsync(wasteType);
            return Ok();
        }
        [HttpGet]
        public async Task<List<WasteTypeDto>> GetWasteType()
        {
            var result = await _wasteTypeService.GetAllAsync();
            return result;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteWasteType([FromBody] WasteTypeDto wasteType)
        {
            await _wasteTypeService.DeleteAsync(wasteType);
            return Ok();
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateWasteType([FromBody] WasteTypeDto wasteType)
        {
            await _wasteTypeService.UpdateAsync(wasteType);
            return Ok();
        }
    }
}
