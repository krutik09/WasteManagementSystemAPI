using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.WasteUnitService;

namespace WasteManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteUnitController : ControllerBase
    {
        private readonly IWasteUnitService _wasteUnitService;
        public WasteUnitController(IWasteUnitService wasteUnitService)
        {
            _wasteUnitService = wasteUnitService;
        }
        [HttpGet]
        public async Task<List<WasteUnitDto>> GetWasteUnit()
        {
            var result =  await _wasteUnitService.GetAllAsync();
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> AddWasteUnit([FromBody] WasteUnitDto wasteType)
        {
            await _wasteUnitService.AddAsync(wasteType);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteWasteUnit([FromBody] WasteUnitDto wasteType)
        {
            await _wasteUnitService.DeleteAsync(wasteType);
            return Ok();
        }
    }
}
