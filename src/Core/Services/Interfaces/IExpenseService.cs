using Core.Entities;
using Core.Infrastructure;

namespace Core.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDto>> GetAllAsync();
        Task<ExpenseDto> GetByIdAsync(Guid id);
        Task CreateAsync(Expense expense);
        Task UpdateAsync(UpdateExpenseDto updateExpenseDto, Guid Id);
        Task DeleteExpenseAsync(Guid id);
    }
}
