
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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
        var result =  await _dbset.FindAsync(id);
        return result;
    }
    public async Task<T?> GetEntity(T entity)
    {
        try
        {
            // Gets the type of entity. Ex: UserType
            var entityType = typeof(T);

            // Gets the list of properties from type. Ex: Id, Name
            var properties = entityType.GetProperties();
            foreach (var property in properties)
            {
                // Create paramater x for expression. Ex: x 
                var parameter = Expression.Parameter(entityType, "x");

                // Attaching property with expression. Ex: x.Id (assuming current property is Id)
                var propertyExpression = Expression.Property(parameter, property.Name);

                // Getting value of property. Ex: Id = 1
                var value = property.GetValue(entity);

                // Hold value in expression. Ex: 1
                var valueExpression = Expression.Constant(value);

                // Creates equal expression using propertyExpression and value. Ex: x.Id == 1
                var equalExpression = Expression.Equal(propertyExpression, valueExpression);

                // Creates lambda function. Ex: x => x.Id == 1
                var lambaExpression = Expression.Lambda<Func<T, bool>>(equalExpression, parameter);

                // Try to find an entity matching this property
                var actualEntity = await _dbset.FirstOrDefaultAsync(lambaExpression);
                if (actualEntity != null)
                {
                    return actualEntity;
                }
            }
        }
        catch(Exception ex)
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
