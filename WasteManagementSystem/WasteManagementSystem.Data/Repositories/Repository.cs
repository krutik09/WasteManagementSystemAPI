
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WasteManagementSystem.Data.Builders;
using WasteManagementSystem.Data.Context;

namespace WasteManagementSystem.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly WmsContext _wmsContext;
    private readonly DbSet<T> _dbset;

    public Repository(WmsContext wmsContext)
    {
        _wmsContext = wmsContext;
        _dbset = _wmsContext.Set<T>();
    }
    public async Task<T> GetByIdAsync(int id)
    {
        var result = await _dbset.FindAsync(id);
        return result;
    }
    public async Task<T?> GetEntity(T entity, string PropertyName)
    {
        try
        {
            // Gets the type of entity. Ex: UserType
            var entityType = typeof(T);
            var property = entityType.GetProperty(PropertyName);
            var value = property.GetValue(entity);
            var actualEntity = await _dbset.FirstOrDefaultAsync(ExpressionBuilder<T>.Build(PropertyName, value));
            if (actualEntity != null)
            {
                return actualEntity;
            }

        }
        catch (Exception ex)
        {
            return null;
        }
        return null;
    }
    public async Task<List<T>> GetAllAsync()
    {
        return await _dbset.AsQueryable().ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbset.AddAsync(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        _dbset.Remove(entity);
    }
    public async Task UpdateAsync(T entity)
    {
        _dbset.Update(entity);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbset.AnyAsync(predicate);
    }

    public async Task SaveChangesAsync()
    {
        await _wmsContext.SaveChangesAsync();
    }


}
