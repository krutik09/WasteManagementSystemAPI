

using AutoMapper;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.BaseService;
using WasteManagementSystem.Data.Models;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Services.UserService;
public interface IUserTypeService : IBaseService<UserType, UserTypeDto> { }
public class UserTypeService:BaseService<UserType,UserTypeDto>, IUserTypeService
{
    public UserTypeService(IRepository<UserType> repository,IMapper mapper):base(repository, mapper)
    {
        _distinctPropertyName = "Name";
    }
}

