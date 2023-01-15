using Core.Entities;
using Core.Entities.Interfaces;
using Core.Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Core.Infrastructure.Repositories
{
    public class ExpenseTotalRepository : IExpenseTotalRepository
    {
        private readonly IRepository<ExpenseTotal> _expenseTotalRepository;

        public ExpenseTotalRepository(IRepository<ExpenseTotal> expenseTotalRepository)
        {
            _expenseTotalRepository = expenseTotalRepository;
        }

        public async Task CreateAsync(ExpenseTotal expenseTotal)
        {
            await _expenseTotalRepository.CreateAsync(expenseTotal);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _expenseTotalRepository.RemoveAsync(id);
        }

        public async Task<IEnumerable<ExpenseTotal>> GetAllAsync()
        {
            return await _expenseTotalRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ExpenseTotal>> GetAllByFilterAsync(Expression<Func<ExpenseTotal, bool>> filter)
        {
            return await _expenseTotalRepository.GetAllAsync(filter);
        }

        public async Task<IEnumerable<ExpenseTotal>> GetAllByFilterAsync(FilterDefinition<ExpenseTotal> filter)
        {
            return await _expenseTotalRepository.GetAllAsync(filter);
        }

        public async Task<ExpenseTotal> GetByFilterAsync(FilterDefinition<ExpenseTotal> filter)
        {
            return await _expenseTotalRepository.GetAsync(filter);
        }

        public async Task<ExpenseTotal> GetByIdAsync(Guid id)
        {
            return await _expenseTotalRepository.GetAsync(id);
        }

        public async Task UpdateExpenseAsync(ExpenseTotal expenseTotal)
        {
            await _expenseTotalRepository.UpdateAsync(expenseTotal);
        }
    }
}
