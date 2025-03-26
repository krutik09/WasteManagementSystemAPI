using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.StatusService;
using WasteManagementSystem.Data.Models;

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
        [HttpGet("{id}")]
        public async Task<Status?> GetStatus(int id)
        {
            var result = await _statusService.GetStatusById(id);
            return result;
        }
        [HttpGet]
        public async Task<List<Status>> GetStatus()
        {
            var result = await _statusService.GetStatus();
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> AddStatus([FromBody] StatusDto status)
        {
            await _statusService.AddStatus(status);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var result = await _statusService.DeleteStatus(id);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateStatus([FromBody] Status status)
        {
            var result  = await _statusService.UpdateStatus(status);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
