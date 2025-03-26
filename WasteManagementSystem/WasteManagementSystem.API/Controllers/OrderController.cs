using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.OrderService;
using WasteManagementSystem.Business.Services.UserService;

namespace WasteManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public OrderController(IOrderService orderService,IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }
        [HttpGet("{id}")]
        public async Task<OrderDto?> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderById(id);
            return result;
        }
        [HttpGet]
        public async Task<List<OrderDto>> GetOrder()
        {
            var loggedInUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userTypeName = (await _userService.GetUserById(loggedInUserId))!.UserTypeName;
            var result = new List<OrderDto>();
            if (userTypeName == "Admin")
            {
                 result = await _orderService.GetOrder();
            }
            else if(userTypeName == "Customer")
            {
                result = await _orderService.GetOrderByUserId(loggedInUserId);
            }
            else if (userTypeName == "Driver")
            {
                result = await _orderService.GetOrderByDriverId(loggedInUserId);
            }
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> Addorder(OrderRequestDto orderRequestDto)
        {
            var loggedInUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _orderService.AddOrder(orderRequestDto, loggedInUserId);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);
            if (!result)
            {
                return BadRequest();    
            }
            return Ok();
        }
        [HttpPost("Update/{id}")]
        public async Task<IActionResult> UpdateOrder(int id,[FromBody] OrderRequestDto order)
        {
            var result = await _orderService.UpdateOrder(id, order);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost("UpdateStatus/{orderId}/{statusId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, int statusId)
        {
            var result = await _orderService.UpdateStatus(orderId,statusId);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost("UpdateDriver/{orderId}/{driverId}")]
        public async Task<IActionResult> UpdateDriver(int orderId, int driverId)
        {
            var result = await _orderService.UpdateDriver(orderId, driverId);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
