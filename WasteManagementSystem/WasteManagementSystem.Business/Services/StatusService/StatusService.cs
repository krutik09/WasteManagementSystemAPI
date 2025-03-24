using AutoMapper;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.BaseService;
using WasteManagementSystem.Data.Models;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Services.StatusService;
public interface IStatusService : IBaseService<Status, StatusDto> { }
public class StatusService:BaseService<Status,StatusDto>, IStatusService
{
    public StatusService(IRepository<Status> repository,IMapper mapper):base(repository,mapper)
    {
        
    }
}
