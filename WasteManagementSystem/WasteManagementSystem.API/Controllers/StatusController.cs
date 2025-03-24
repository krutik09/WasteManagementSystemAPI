using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.StatusService;

namespace WasteManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }
        [HttpGet]
        public async Task<List<StatusDto>> GetStatus()
        {
            var result = await _statusService.GetAllAsync();
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> AddStatus([FromBody] StatusDto status)
        {
            await _statusService.AddAsync(status);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteStatus([FromBody] StatusDto status)
        {
            await _statusService.DeleteAsync(status);
            return Ok();
        }
    }
}
