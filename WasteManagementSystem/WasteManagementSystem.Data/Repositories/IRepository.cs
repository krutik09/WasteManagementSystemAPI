using System.Linq.Expressions;

namespace WasteManagementSystem.Data.Repositories;

public interface IRepository<T>
{
    Task<T?> GetEntity(T entity,string propertyName);
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task SaveChangesAsync();
}

