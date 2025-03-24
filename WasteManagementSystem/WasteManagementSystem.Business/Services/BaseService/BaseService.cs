

using AutoMapper;
using WasteManagementSystem.Data.Repositories;

namespace WasteManagementSystem.Business.Services.BaseService;

public class BaseService<T, TDto> : IBaseService<T, TDto>
{
    private readonly IRepository<T> _repository;
    public string _distinctPropertyName;
    private readonly IMapper _mapper;
    public BaseService(IRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<TDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        var entityDto = _mapper.Map<TDto>(entity);
        return entityDto;
    }
    public async Task<List<TDto>> GetAllAsync()
    {
        var entityList = await _repository.GetAllAsync();
        var resultantListOfEntityDtos = new List<TDto>();
        foreach (var entity in entityList)
        {
            var entityDto = _mapper.Map<TDto>(entity);
            resultantListOfEntityDtos.Add(entityDto);
        }
        return resultantListOfEntityDtos;
    }

    public async Task AddAsync(TDto entity)
    {
        var _entity = _mapper.Map<T>(entity);
        await _repository.AddAsync(_entity);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(TDto entity)
    {

        var _entity = _mapper.Map<T>(entity);
        var actual_entity = await _repository.GetEntity(_entity, _distinctPropertyName);
        await _repository.DeleteAsync(actual_entity);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(TDto entity)
    {
        var _entity = _mapper.Map<T>(entity);
        var actual_entity = await _repository.GetEntity(_entity, _distinctPropertyName);
        await _repository.UpdateAsync(actual_entity);
        await _repository.SaveChangesAsync();
    }
}

