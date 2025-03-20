namespace WasteManagementSystem.Business.Services.BaseService;

public interface IBaseService<T, TDto>
{
    Task<TDto> GetByIdAsync(int id);
    Task<List<TDto>> GetAllAsync();
    Task AddAsync(TDto entity);
    Task UpdateAsync(TDto entity);
    Task DeleteAsync(TDto entity);
}

