using MongoDB.Driver;
using System.Linq.Expressions;

namespace Core.Entities.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(FilterDefinition<T> filter);
        Task<IReadOnlyCollection<T>> GetAllAsync(FilterDefinition<T> filter);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(T entity);
    }
}
