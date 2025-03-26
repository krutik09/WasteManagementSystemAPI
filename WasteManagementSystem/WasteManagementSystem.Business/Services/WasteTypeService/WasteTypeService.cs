using AutoMapper;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Data.Models;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Services.WasteTypeService;
public interface IWasteTypeService {
    Task<WasteType?> GetWasteTypeById(int id);
    Task<List<WasteType>> GetWasteType();
    Task AddWasteUnit(WasteTypeDto wasteType);
    Task<bool> DeleteWasteUnit(int id);
    Task<bool> UpdateWasteUnit(WasteType wasteType);
}
public class WasteTypeService:IWasteTypeService
{
    private readonly IRepository<WasteType> _wasteTypeRepository;
    private readonly IMapper _mapper;
    public WasteTypeService(IRepository<WasteType> wasteTypeRepository, IMapper mapper)
    {
        _wasteTypeRepository = wasteTypeRepository;
        _mapper = mapper;
    }
    public async Task<WasteType?> GetWasteTypeById(int id)
    {
        return await _wasteTypeRepository.GetByIdAsync(id);
    }
    public async Task<List<WasteType>> GetWasteType()
    {
        var wasteUnitList = await _wasteTypeRepository.GetAllAsync();
        return wasteUnitList;
    }
    public async Task AddWasteUnit(WasteTypeDto wasteType)
    {
        var wasteUnitObject = _mapper.Map<WasteType>(wasteType);
        await _wasteTypeRepository.AddAsync(wasteUnitObject);
        await _wasteTypeRepository.SaveChangesAsync();
    }
    public async Task<bool> DeleteWasteUnit(int id)
    {
        var wasteType = await GetWasteTypeById(id);
        if (wasteType == null) { return false; }
        await _wasteTypeRepository.DeleteAsync(wasteType);
        await _wasteTypeRepository.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateWasteUnit(WasteType wasteType)
    {
        if(await GetWasteTypeById(wasteType.Id) == null)
        {
            return false;
        }
        await _wasteTypeRepository.UpdateAsync(wasteType);
        await _wasteTypeRepository.SaveChangesAsync();
        return true;
    }

}

