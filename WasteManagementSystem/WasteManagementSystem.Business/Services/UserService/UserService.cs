using AutoMapper;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.BaseService;
using WasteManagementSystem.Data.Models;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Services.UserService;

public interface IUserService
{
    Task<UserDto?> GetUserById(int id);
    Task<List<UserDto>> GetUser();
    Task AddUser(UserRequestDto userDto);
    Task<bool> DeleteUser(int id);
    Task<bool> UpdateUser(int id,UserRequestDto user);

}
public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserType> _userTypeRepository;
    public UserService(IRepository<User> userRepository, IRepository<UserType> userTypeRepository, IMapper mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userTypeRepository = userTypeRepository;
    }
    public async Task<UserDto?> GetUserById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user != null)
        {
            var userDto = _mapper.Map<UserDto>(user);
            userDto.UserTypeName = (await _userTypeRepository.GetByIdAsync(user.UserTypeId)).Name;
            return userDto;
        }
        return null;
    }
    public async Task<List<UserDto>> GetUser()
    {
        var userList = await _userRepository.GetAllAsync();
        var userTypeList = await _userTypeRepository.GetAllAsync();
        var result = (from u in userList
                     join
                     ut in userTypeList on u.UserTypeId equals ut.Id
                     select new UserDto
                     {
                         Id = u.Id,
                         Name = u.Name,
                         Email = u.Email,
                         PhoneNumber = u.PhoneNumber,
                         UserTypeId = u.UserTypeId,
                         UserTypeName = ut.Name,
                     }).ToList();
        return result;
    }
    public async Task AddUser(UserRequestDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }
    public async Task<bool> DeleteUser(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return false;
        }
        await _userRepository.DeleteAsync(user);
        await _userRepository.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateUser(int id,UserRequestDto user)
    {
        var actual_user = await _userRepository.GetByIdAsync(id);
        if (actual_user == null)
        {
            return false;
        }
        actual_user.Name = user.Name;
        actual_user.Password = user.Password;
        actual_user.Email = user.Email;
        actual_user.PhoneNumber = user.PhoneNumber;
        actual_user.UserTypeId = user.UserTypeId;
        await _userRepository.UpdateAsync(actual_user);
        await _userRepository.SaveChangesAsync();
        return true;
    }

}

