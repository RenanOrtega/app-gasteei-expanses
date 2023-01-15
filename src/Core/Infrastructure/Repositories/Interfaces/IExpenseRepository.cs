using Core.Entities;
using System.Linq.Expressions;

namespace Core.Infrastructure.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        Task<Expense> GetByFilterAsync(Expression<Func<Expense, bool>> filter);
        Task CreateAsync(Expense expense);
        Task DeleteExpenseAsync(Guid id);
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<Expense> GetByIdAsync(Guid id);
        Task UpdateExpenseAsync(Expense expense);
    }
}