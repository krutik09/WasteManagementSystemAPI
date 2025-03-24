using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.BaseService;
using WasteManagementSystem.Data.Context;
using WasteManagementSystem.Data.Models;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Services.UserService;

public interface IUserService : IBaseService<User, UserDto> {
    Task<List<UserDto>> GetAllUser();
    Task AddUser(UserDto userDto);
}
public class UserService:BaseService<User,UserDto>,IUserService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserType> _userTypeRepository;
    public UserService(IRepository<User> userRepository, IRepository<UserType> userTypeRepository, IMapper mapper):base(userRepository, mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userTypeRepository= userTypeRepository;
        _distinctPropertyName = "Email";
    }
    public async Task<List<UserDto>> GetAllUser()
    {
        var result = new List<UserDto>();
        var userList = await _userRepository.GetAllAsync();
        foreach (var user in userList) { 
            var userDto = _mapper.Map<UserDto>(user);
            var userType = await _userTypeRepository.GetByIdAsync(user.UserTypeId);
            userDto.UserTypeName = userType.Name;
            result.Add(userDto);
        }
        return result;
    }
    public async Task AddUser(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var userTypeList = await _userTypeRepository.GetAllAsync();
        user.UserTypeId = userTypeList.FirstOrDefault(x => x.Name == userDto.UserTypeName).Id;
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }


}

