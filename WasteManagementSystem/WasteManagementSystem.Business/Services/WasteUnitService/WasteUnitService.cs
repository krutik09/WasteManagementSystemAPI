using AutoMapper;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.BaseService;
using WasteManagementSystem.Data.Models;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Services.WasteUnitService;
public interface IWasteUnitService : IBaseService<WasteUnit, WasteUnitDto> { }
public class WasteUnitService : BaseService<WasteUnit, WasteUnitDto>, IWasteUnitService
{
    public WasteUnitService(IRepository<WasteUnit> repository, IMapper mapper) : base(repository, mapper)
    {
        
    }

}

