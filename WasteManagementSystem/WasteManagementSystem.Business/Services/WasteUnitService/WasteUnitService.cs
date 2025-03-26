using AutoMapper;
using WasteManagementSystem.Business.DTO;
using WasteManagementSystem.Business.Services.BaseService;
using WasteManagementSystem.Data.Models;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Services.WasteUnitService;
public interface IWasteUnitService
{
    Task<WasteUnit?> GetWasteUnitById(int id);
    Task<List<WasteUnit>> GetWasteUnit();
    Task AddWasteUnit(WasteUnitDto wasteUnit);
    Task<bool> DeleteWasteUnit(int id);
    Task<bool> UpdateWasteUnit(WasteUnit wasteUnit);

}
public class WasteUnitService : IWasteUnitService
{
    private readonly IRepository<WasteUnit> _wasteUnitRepository;
    private readonly IMapper _mapper;
    public WasteUnitService(IRepository<WasteUnit> wasteUnitRepository, IMapper mapper)
    {
        _wasteUnitRepository = wasteUnitRepository; 
        _mapper = mapper;
    }
    public async Task<WasteUnit?> GetWasteUnitById(int id)
    {
        return await _wasteUnitRepository.GetByIdAsync(id);
    }
    public async Task<List<WasteUnit>> GetWasteUnit()
    {
        var wasteUnitList = await _wasteUnitRepository.GetAllAsync();
        return wasteUnitList;
    }
    public async Task AddWasteUnit(WasteUnitDto wasteUnit)
    {
        var wasteUnitObject = _mapper.Map<WasteUnit>(wasteUnit);
        await _wasteUnitRepository.AddAsync(wasteUnitObject);
        await _wasteUnitRepository.SaveChangesAsync();
    }
    public async Task<bool> DeleteWasteUnit(int id)
    {
        var wasteUnit = await GetWasteUnitById(id);
        if (wasteUnit == null) { return false; }
        await _wasteUnitRepository.DeleteAsync(wasteUnit);
        await _wasteUnitRepository.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateWasteUnit(WasteUnit wasteUnit)
    {
        if(await GetWasteUnitById(wasteUnit.Id) == null)
        {
            return false;
        }
        await _wasteUnitRepository.UpdateAsync(wasteUnit);
        await _wasteUnitRepository.SaveChangesAsync();
        return true;

    }
}

