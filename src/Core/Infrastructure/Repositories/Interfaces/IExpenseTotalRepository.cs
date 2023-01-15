using Core.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Core.Infrastructure.Repositories.Interfaces
{
    public interface IExpenseTotalRepository
    {
        Task<IEnumerable<ExpenseTotal>> GetAllByFilterAsync(Expression<Func<ExpenseTotal, bool>> filter);
        Task CreateAsync(ExpenseTotal expense);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ExpenseTotal>> GetAllAsync();
        Task<ExpenseTotal> GetByIdAsync(Guid id);
        Task UpdateExpenseAsync(ExpenseTotal expenseTotal);
        Task<IEnumerable<ExpenseTotal>> GetAllByFilterAsync(FilterDefinition<ExpenseTotal> filter);
        Task<ExpenseTotal> GetByFilterAsync(FilterDefinition<ExpenseTotal> filter);
    }
}