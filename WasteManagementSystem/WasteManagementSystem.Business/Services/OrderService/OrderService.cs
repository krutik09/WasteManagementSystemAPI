using AutoMapper;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.BaseService;
using WasteManagementSystem.Data.Models;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Services.OrderService;
public interface IOrderService : IBaseService<Order, OrderDto>
{
    Task<List<OrderDto>> GetAllOrder();
    Task AddOrder(OrderRequestDto orderRequest, int LoggedInUserId);
}
public class OrderService : BaseService<Order, OrderDto>, IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<WasteUnit> _wasteUnitRepository;
    private readonly IRepository<WasteType> _wasteTypeRepository;
    private readonly IRepository<Status> _statusRepository;

    private readonly IMapper _mapper;

    public OrderService(IRepository<Order> orderRepository, IRepository<User> userRepository, IRepository<WasteUnit> wasteUnitRepository, IRepository<WasteType> wasteTypeRepository, IRepository<Status> statusRepository, IMapper mapper) : base(orderRepository, mapper)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _wasteUnitRepository = wasteUnitRepository;
        _wasteTypeRepository = wasteTypeRepository;
        _statusRepository = statusRepository;
        _mapper = mapper;
        _distinctPropertyName = "OrderId";
    }
    public async Task<List<OrderDto>> GetAllOrder()
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
                       OrderId = o.OrderId,
                       OrderName = o.OrderName,
                       OrderDescription = o.OrderDescription,
                       OrderDate = o.OrderDate,
                       WasteTypeName = wt.Name,
                       WasteUnitName = wu.Name,
                       WasteAmount = o.WasteAmount,
                       UserName = u.Name,
                       DriverName = d.Name,
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
                                               OrderId = o.OrderId,
                                               OrderName = o.OrderName,
                                               OrderDescription = o.OrderDescription,
                                               OrderDate = o.OrderDate,
                                               WasteTypeName = wt.Name,
                                               WasteUnitName = wu.Name,
                                               UserName = u.Name,
                                               StatusName = s.Name
                                           }).ToList();
        return orderListWithDriverAssgined.Concat(orderListWithoutDriverAssgined).ToList();
    }
    public async Task AddOrder(OrderRequestDto orderRequest, int LoggedInUserId)
    {
        var wasteTypeList = await _wasteTypeRepository.GetAllAsync();
        var wasteUnitList = await _wasteUnitRepository.GetAllAsync();
        var order = _mapper.Map<Order>(orderRequest);
        order.WasteTypeId = wasteTypeList.FirstOrDefault(x => x.Name == orderRequest.WasteTypeName).Id;
        order.WasteUnitId = wasteUnitList.FirstOrDefault(x => x.Name == orderRequest.WasteUnitName).Id;
        order.UserId = LoggedInUserId;
        order.DriverId = null;
        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();
    }
}

