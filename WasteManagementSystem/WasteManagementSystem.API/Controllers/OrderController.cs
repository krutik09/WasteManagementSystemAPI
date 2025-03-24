using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.OrderService;

namespace WasteManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("admin")]
        public async Task<List<OrderDto>> GetOrder()
        {
            var result = await _orderService.GetAllOrder();
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> Addorder(OrderRequestDto orderRequestDto)
        {
            var loggedInUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _orderService.AddOrder(orderRequestDto, loggedInUserId);
            return Ok();
        }
    }
}
