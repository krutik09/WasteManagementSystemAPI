
using AutoMapper;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.BaseService;
using WasteManagementSystem.Data.Models;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Services.WasteTypeService;
public interface IWasteTypeService : IBaseService<WasteType, WasteTypeDto> { }
public class WasteTypeService:BaseService<WasteType,WasteTypeDto>,IWasteTypeService
{
    public WasteTypeService(IRepository<WasteType> repository,IMapper mapper):base(repository,mapper)
    {
        
    }

}

