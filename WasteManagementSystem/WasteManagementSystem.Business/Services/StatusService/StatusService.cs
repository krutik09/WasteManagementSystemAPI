using AutoMapper;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Data.Models;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Services.StatusService;
public interface IStatusService {
    Task<Status?> GetStatusById(int id);
    Task<List<Status>> GetStatus();
    Task AddStatus(StatusDto Status);
    Task<bool> DeleteStatus(int id);
    Task<bool> UpdateStatus(Status Status);
}
public class StatusService: IStatusService
{
    private readonly IRepository<Status> _statusRepository; 
    private readonly IMapper _mapper;
    public StatusService(IRepository<Status> statusRepository,IMapper mapper)
    {
        _statusRepository = statusRepository;
        _mapper = mapper;
    }
    public async Task<Status?> GetStatusById(int id)
    {
        return await _statusRepository.GetByIdAsync(id);
    }
    public async Task<List<Status>> GetStatus()
    {
        var wasteUnitList = await _statusRepository.GetAllAsync();
        return wasteUnitList;
    }
    public async Task AddStatus(StatusDto Status)
    {
        var wasteUnitObject = _mapper.Map<Status>(Status);
        await _statusRepository.AddAsync(wasteUnitObject);
        await _statusRepository.SaveChangesAsync();
    }
    public async Task<bool> DeleteStatus(int id)
    {
        var status = await GetStatusById(id);
        if (status == null) {
            return false;
        }
        await _statusRepository.DeleteAsync(status);
        await _statusRepository.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateStatus(Status Status)
    {
        if(await GetStatusById(Status.Id) == null)
        {
            return false;
        }
        await _statusRepository.UpdateAsync(Status);
        await _statusRepository.SaveChangesAsync();
        return true;
    }
}
