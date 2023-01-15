using Core.Entities;
using Core.Entities.Interfaces;
using Core.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Core.Infrastructure.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IRepository<Expense> _expenseRepository;

        public ExpenseRepository(IRepository<Expense> expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<Expense> GetByFilterAsync(Expression<Func<Expense, bool>> filter)
        {
            return await _expenseRepository.GetAsync(filter);
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _expenseRepository.GetAllAsync();
        }

        public async Task<Expense> GetByIdAsync(Guid id)
        {
            return await _expenseRepository.GetAsync(id);
        }

        public async Task CreateAsync(Expense expense)
        {
            await _expenseRepository.CreateAsync(expense);
        }

        public async Task DeleteExpenseAsync(Guid id)
        {
            await _expenseRepository.RemoveAsync(id);
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            await _expenseRepository.UpdateAsync(expense);
        }
    }
}
