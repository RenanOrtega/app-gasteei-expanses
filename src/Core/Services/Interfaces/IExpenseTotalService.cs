using Core.Entities;
using Core.Infrastructure;

namespace Core.Services.Interfaces
{
    public interface IExpenseTotalService
    {
        Task<ExpenseTotalDto> GetAsync(Guid id);
        Task<IEnumerable<ExpenseTotal>> GetAllExpensesTotal(string email, DateTime startDate, DateTime endDate);
        Task AddToTotal(double newValue, ExpenseTotal expenseTotal);
        Task DecreaseFromTotal(double newValue, ExpenseTotal expenseTotal);
        Task CreateExpenseTotalIfNotExists(Expense expense);
        Task FirstExpenseTotalCreation(ExpenseTotal expenseTotal);
        Task<IEnumerable<ExpenseTotalDto>> GetAllAsync();
    }
}
