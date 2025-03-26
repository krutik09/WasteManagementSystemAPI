using AutoMapper;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Data.Models;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Services.OrderService;
public interface IOrderService
{
    Task<OrderDto?> GetOrderById(int id);
    Task<List<OrderDto>> GetOrder();
    Task<List<OrderDto>> GetOrderByUserId(int userId);
    Task<List<OrderDto>> GetOrderByDriverId(int driverId);
    Task AddOrder(OrderRequestDto orderRequest, int LoggedInUserId);
    Task<bool> DeleteOrder(int id);
    Task<bool> UpdateOrder(int id, OrderRequestDto orderReq);
    Task<bool> UpdateStatus(int orderId,int statusId);
    Task<bool> UpdateDriver(int orderId, int driverId);
}
public class OrderService: IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<WasteUnit> _wasteUnitRepository;
    private readonly IRepository<WasteType> _wasteTypeRepository;
    private readonly IRepository<Status> _statusRepository;

    private readonly IMapper _mapper;

    public OrderService(IRepository<Order> orderRepository, IRepository<User> userRepository, IRepository<WasteUnit> wasteUnitRepository, IRepository<WasteType> wasteTypeRepository, IRepository<Status> statusRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _wasteUnitRepository = wasteUnitRepository;
        _wasteTypeRepository = wasteTypeRepository;
        _statusRepository = statusRepository;
        _mapper = mapper;
    }
    public async Task<OrderDto?> GetOrderById(int id)
    {
        var orderList = new List<Order>{
            await _orderRepository.GetByIdAsync(id) 
        };
        if (orderList.Count > 0)
        {
            var wasteTypeList = await _wasteTypeRepository.GetAllAsync();
            var wasteUnitList = await _wasteUnitRepository.GetAllAsync();
            var userList = await _userRepository.GetAllAsync();
            var statusList = await _statusRepository.GetAllAsync();
            if (orderList[0].DriverId != null)
            {
                var result = (from o in orderList
                             join
                                wt in wasteTypeList on o.WasteTypeId equals wt.Id
                             join
                                wu in wasteUnitList on o.WasteUnitId equals wu.Id
                             join
                                u in userList on o.UserId equals u.Id
                             join
                                s in statusList on o.StatusId equals s.Id
                             join
                                d in userList on o.DriverId equals d.Id
                             select new OrderDto
                             {
                                 Id = o.Id,
                                 OrderId = o.OrderId,
                                 OrderName = o.OrderName,
                                 OrderDescription = o.OrderDescription,
                                 OrderDate = o.OrderDate,
                                 WasteTypeId = o.WasteTypeId,
                                 WasteTypeName = wt.Name,
                                 WasteUnitId = wu.Id,
                                 WasteUnitName = wu.Name,
                                 WasteAmount = o.WasteAmount,
                                 UserId = o.UserId,
                                 UserName = u.Name,
                                 DriverId = o.DriverId,
                                 DriverName = d.Name,
                                 StatusId = o.StatusId,
                                 StatusName = s.Name
                             }).First();
                return result;
            }
            else
            {
                var result = (from o in orderList
                              join
                                 wt in wasteTypeList on o.WasteTypeId equals wt.Id
                              join
                                 wu in wasteUnitList on o.WasteUnitId equals wu.Id
                              join
                                 u in userList on o.UserId equals u.Id
                              join
                                 s in statusList on o.StatusId equals s.Id
                              select new OrderDto
                              {
                                  Id = o.Id,
                                  OrderId = o.OrderId,
                                  OrderName = o.OrderName,
                                  OrderDescription = o.OrderDescription,
                                  OrderDate = o.OrderDate,
                                  WasteTypeId = o.WasteTypeId,
                                  WasteTypeName = wt.Name,
                                  WasteUnitId = wu.Id,
                                  WasteUnitName = wu.Name,
                                  WasteAmount = o.WasteAmount,
                                  UserId = o.UserId,
                                  UserName = u.Name,
                                  StatusId = o.StatusId,
                                  StatusName = s.Name
                              }).First();
                return result;
            }
        }
        return null;
    }
    public async Task<List<OrderDto>> GetOrder()
    {
        var orderList = await _orderRepository.GetAllAsync();
        var wasteTypeList = await _wasteTypeRepository.GetAllAsync();
        var wasteUnitList = await _wasteUnitRepository.GetAllAsync();
        var userList = await _userRepository.GetAllAsync();
        var statusList = await _statusRepository.GetAllAsync();
        var orderListWithDriverAssgined = (from o in orderList
                   join
                         wt in wasteTypeList on o.WasteTypeId equals wt.Id
                   join
                         wu in wasteUnitList on o.WasteUnitId equals wu.Id
                   join
                         u in userList on o.UserId equals u.Id

                   join
                         s in statusList on o.StatusId equals s.Id
                   join
                   d in userList on o.DriverId equals d.Id
                   where o.DriverId != null
                   select new OrderDto
                   {
                       Id = o.Id,
                       OrderId = o.OrderId,
                       OrderName = o.OrderName,
                       OrderDescription = o.OrderDescription,
                       OrderDate = o.OrderDate,
                       WasteTypeId = o.WasteTypeId,
                       WasteTypeName = wt.Name,
                       WasteUnitId = wu.Id,
                       WasteUnitName = wu.Name,
                       WasteAmount = o.WasteAmount,
                       UserId = o.UserId,
                       UserName = u.Name,
                       DriverId = o.DriverId,
                       DriverName = d.Name,
                       StatusId = o.StatusId,
                       StatusName = s.Name
                   }).ToList();
        var orderListWithoutDriverAssgined = (from o in orderList
                                           join
                                                 wt in wasteTypeList on o.WasteTypeId equals wt.Id
                                           join
                                                 wu in wasteUnitList on o.WasteUnitId equals wu.Id
                                           join
                                                 u in userList on o.UserId equals u.Id

                                           join
                                                 s in statusList on o.StatusId equals s.Id
                                           select new OrderDto
                                           {
                                               Id = o.Id,
                                               OrderId = o.OrderId,
                                               OrderName = o.OrderName,
                                               OrderDescription = o.OrderDescription,
                                               OrderDate = o.OrderDate,
                                               WasteTypeId = o.WasteTypeId,
                                               WasteTypeName = wt.Name,
                                               WasteUnitId = wu.Id,
                                               WasteUnitName = wu.Name,
                                               WasteAmount = o.WasteAmount,
                                               UserId = o.UserId,
                                               UserName = u.Name,
                                               StatusId= o.StatusId,
                                               StatusName = s.Name
                                           }).ToList();
        return orderListWithDriverAssgined.Concat(orderListWithoutDriverAssgined).ToList();
    }
    public async Task<List<OrderDto>> GetOrderByUserId(int userId)
    {
        var orderList = await _orderRepository.GetAllAsync();
        var wasteTypeList = await _wasteTypeRepository.GetAllAsync();
        var wasteUnitList = await _wasteUnitRepository.GetAllAsync();
        var userList = await _userRepository.GetAllAsync();
        var statusList = await _statusRepository.GetAllAsync();
        var orderListWithDriverAssgined = (from o in orderList
                                           join
                                                 wt in wasteTypeList on o.WasteTypeId equals wt.Id
                                           join
                                                 wu in wasteUnitList on o.WasteUnitId equals wu.Id
                                           join
                                                 u in userList on o.UserId equals u.Id

                                           join
                                                 s in statusList on o.StatusId equals s.Id
                                           join
                                           d in userList on o.DriverId equals d.Id
                                           where o.DriverId!=null && u.Id == userId
                                           select new OrderDto
                                           {
                                               Id = o.Id,
                                               OrderId = o.OrderId,
                                               OrderName = o.OrderName,
                                               OrderDescription = o.OrderDescription,
                                               OrderDate = o.OrderDate,
                                               WasteTypeId = o.WasteTypeId,
                                               WasteTypeName = wt.Name,
                                               WasteUnitId = wu.Id,
                                               WasteUnitName = wu.Name,
                                               WasteAmount = o.WasteAmount,
                                               UserId = o.UserId,
                                               UserName = u.Name,
                                               DriverId = o.DriverId,
                                               DriverName = d.Name,
                                               StatusId = o.StatusId,
                                               StatusName = s.Name
                                           }).ToList();
        var orderListWithoutDriverAssgined = (from o in orderList
                                              join
                                                    wt in wasteTypeList on o.WasteTypeId equals wt.Id
                                              join
                                                    wu in wasteUnitList on o.WasteUnitId equals wu.Id
                                              join
                                                    u in userList on o.UserId equals u.Id

                                              join
                                                    s in statusList on o.StatusId equals s.Id
                                              where o.DriverId == null && u.Id == userId
                                              select new OrderDto
                                              {
                                                  Id = o.Id,
                                                  OrderId = o.OrderId,
                                                  OrderName = o.OrderName,
                                                  OrderDescription = o.OrderDescription,
                                                  OrderDate = o.OrderDate,
                                                  WasteTypeId = o.WasteTypeId,
                                                  WasteTypeName = wt.Name,
                                                  WasteUnitId = wu.Id,
                                                  WasteUnitName = wu.Name,
                                                  WasteAmount = o.WasteAmount,
                                                  UserId = o.UserId,
                                                  UserName = u.Name,
                                                  StatusId = o.StatusId,
                                                  StatusName = s.Name
                                              }).ToList();
        return orderListWithDriverAssgined.Concat(orderListWithoutDriverAssgined).ToList();
    }
    public async  Task<List<OrderDto>> GetOrderByDriverId(int driverId)
    {
        var orderList = (await _orderRepository.GetAllAsync()).Where(x=>x.DriverId!=null&&x.DriverId==driverId).ToList();
        var wasteTypeList = await _wasteTypeRepository.GetAllAsync();
        var wasteUnitList = await _wasteUnitRepository.GetAllAsync();
        var userList = await _userRepository.GetAllAsync();
        var statusList = await _statusRepository.GetAllAsync();
        var result = (from o in orderList
                                           join
                                                 wt in wasteTypeList on o.WasteTypeId equals wt.Id
                                           join
                                                 wu in wasteUnitList on o.WasteUnitId equals wu.Id
                                           join
                                                 u in userList on o.UserId equals u.Id

                                           join
                                                 s in statusList on o.StatusId equals s.Id
                                           join
                                           d in userList on o.DriverId equals d.Id
                                           where o.DriverId != null
                                           select new OrderDto
                                           {
                                               Id = o.Id,
                                               OrderId = o.OrderId,
                                               OrderName = o.OrderName,
                                               OrderDescription = o.OrderDescription,
                                               OrderDate = o.OrderDate,
                                               WasteTypeId = o.WasteTypeId,
                                               WasteTypeName = wt.Name,
                                               WasteUnitId = wu.Id,
                                               WasteUnitName = wu.Name,
                                               WasteAmount = o.WasteAmount,
                                               UserId = o.UserId,
                                               UserName = u.Name,
                                               DriverId = o.DriverId,
                                               DriverName = d.Name,
                                               StatusId = o.StatusId,
                                               StatusName = s.Name
                                           }).ToList();
        return result;
    }
    public async Task AddOrder(OrderRequestDto orderRequest, int LoggedInUserId)
    {   
        var order = _mapper.Map<Order>(orderRequest);
        order.UserId = LoggedInUserId;
        order.DriverId = null;
        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();
    }
    public async Task<bool> DeleteOrder(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) {
            return false;
        }
        await _orderRepository.DeleteAsync(order);
        await _orderRepository.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateOrder(int id,OrderRequestDto orderReq)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            return false;
        }
        order.OrderName = orderReq.OrderName;
        order.OrderDescription = orderReq.OrderDescription;
        order.WasteTypeId = orderReq.WasteTypeId;
        order.WasteAmount = orderReq.WasteAmount;
        order.WasteUnitId = orderReq.WasteUnitId;
        await _orderRepository.UpdateAsync(order);
        await _orderRepository.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateStatus(int orderId, int statusId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null||(order.StatusId == 2 && statusId==4))
        {
            return false;
        }
        order.StatusId = statusId;
        await _orderRepository.UpdateAsync(order);
        await _orderRepository.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateDriver(int orderId, int driverId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            return false;
        }
        order.DriverId = driverId;
        order.StatusId = 2;
        await _orderRepository.UpdateAsync(order);
        await _orderRepository.SaveChangesAsync();
        return true;
    }
}

