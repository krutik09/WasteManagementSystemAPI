using AutoMapper;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.BaseService;
using WasteManagementSystem.Data.Models;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Services.UserService;
public interface IUserTypeService
{
    Task<UserType?> GetUserTypeById(int id);
    Task<List<UserType>> GetUserType();
    Task AddWasteUnit(UserTypeDto userType);
    Task<bool> DeleteWasteUnit(int id);
    Task<bool> UpdateWasteUnit(UserType userType);
}
public class UserTypeService: IUserTypeService
{
    private readonly IRepository<UserType> _userTypeRepository;
    private readonly IMapper _mapper;

    public UserTypeService(IRepository<UserType> userTypeRepository, IMapper mapper)
    {
        _userTypeRepository = userTypeRepository;
        _mapper = mapper;
    }
    public async Task<UserType?> GetUserTypeById(int id)
    {
        return await _userTypeRepository.GetByIdAsync(id);
    }
    public async Task<List<UserType>> GetUserType()
    {
        var wasteUnitList = await _userTypeRepository.GetAllAsync();
        return wasteUnitList;
    }
    public async Task AddWasteUnit(UserTypeDto userType)
    {
        var wasteUnitObject = _mapper.Map<UserType>(userType);
        await _userTypeRepository.AddAsync(wasteUnitObject);
        await _userTypeRepository.SaveChangesAsync();
    }
    public async Task<bool> DeleteWasteUnit(int id)
    {
        var userType = await GetUserTypeById(id);
        if (userType == null) { 
            return false;
        } 
        await _userTypeRepository.DeleteAsync(userType);
        await _userTypeRepository.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateWasteUnit(UserType userType)
    {
        if(await GetUserTypeById(userType.Id) == null) { return false; }
        await _userTypeRepository.UpdateAsync(userType);
        await _userTypeRepository.SaveChangesAsync();
        return true;
    }
}

