using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.OrderService;
using WasteManagementSystem.Business.Services.UserService;

namespace WasteManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
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
            var userTypeName = (await _userService.GetUserById(LoggedInUserId))!.UserTypeName;
            var result = new List<OrderDto>();
            if (userTypeName == "Admin")
            {
                 result = await _orderService.GetOrder();
            }
            else if(userTypeName == "Customer")
            {
                result = await _orderService.GetOrderByUserId(LoggedInUserId);
            }
            else if (userTypeName == "Driver")
            {
                result = await _orderService.GetOrderByDriverId(LoggedInUserId);
            }
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> Addorder(OrderRequestDto orderRequestDto)
        {
            await _orderService.AddOrder(orderRequestDto, LoggedInUserId);
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
            var result = await _orderService.UpdateOrder(id, order,LoggedInUserId);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost("UpdateStatus/{orderId}/{statusId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, int statusId)
        {
            var result = await _orderService.UpdateStatus(orderId,statusId, LoggedInUserId);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateDriver/{orderId}/{driverId}")]
        public async Task<IActionResult> UpdateDriver(int orderId, int driverId)
        {
            var result = await _orderService.UpdateDriver(orderId, driverId, LoggedInUserId);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
