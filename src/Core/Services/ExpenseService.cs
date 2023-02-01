using Core.Entities;
using Core.Infrastructure;
using Core.Infrastructure.Repositories.Interfaces;
using Core.Services.Interfaces;

namespace Core.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseTotalService _expenseTotalService;

        public ExpenseService(
            IExpenseRepository expenseRepository,
            IExpenseTotalService expenseTotalService)
        {
            _expenseRepository = expenseRepository;
            _expenseTotalService = expenseTotalService;
        }

        public async Task<IEnumerable<ExpenseDto>> GetAllAsync()
        {
            var expenses = (await _expenseRepository.GetAllAsync())
                .Select(expense => expense.AsDto());

            return expenses;
        }

        public async Task<ExpenseDto> GetByIdAsync(Guid id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense is null)
                throw new Exception($"Expense with Id = {id} not found");

            return expense.AsDto();
        }

        public async Task CreateAsync(Expense expense)
        {
            await _expenseTotalService.CreateExpenseTotalIfNotExists(expense);

            var expensesTotal = await _expenseTotalService.GetAllExpensesTotal(expense.Email, expense.Start.Date, expense.End.Date);

            foreach (var expenseTotal in expensesTotal)
            {
                await _expenseTotalService.AddToTotal(expense.Value, expenseTotal);
            }

            await _expenseRepository.CreateAsync(expense);
        }

        public async Task DeleteExpenseAsync(Guid id)
        {
            var existingExpense = await _expenseRepository.GetByIdAsync(id);

            if (existingExpense is null)
                throw new Exception($"Expense with Id = {id} not found");

            var expensesTotal = await _expenseTotalService.GetAllExpensesTotal(existingExpense.Email, existingExpense.Start.Date, existingExpense.End.Date);

            foreach (var expenseTotal in expensesTotal)
            {
                await _expenseTotalService.DecreaseFromTotal(existingExpense.Value, expenseTotal);
            }

            await _expenseRepository.DeleteExpenseAsync(id);
        }

        public async Task UpdateAsync(UpdateExpenseDto updateExpenseDto, Guid id)
        {
            var existingExpense = await _expenseRepository.GetByIdAsync(id);

            if (existingExpense is null)
                throw new Exception($"Expense with Id = {id} not found");

            if (existingExpense.Title != updateExpenseDto.Title)
                existingExpense.Title = updateExpenseDto.Title;

            if (existingExpense.Value == updateExpenseDto.Value && existingExpense.Title == updateExpenseDto.Title)
                return;

            var expensesTotal = await _expenseTotalService.GetAllExpensesTotal(existingExpense.Email, existingExpense.Start.Date, existingExpense.End.Date);
            double newValue;

            if (existingExpense.Value < updateExpenseDto.Value)
            {
                newValue = updateExpenseDto.Value - existingExpense.Value;

                foreach (var expenseTotal in expensesTotal)
                {
                    if (expenseTotal.Date < DateTime.UtcNow)
                        continue;

                    await _expenseTotalService.AddToTotal(newValue, expenseTotal);
                }
            }

            if (existingExpense.Value > updateExpenseDto.Value)
            {
                newValue = existingExpense.Value - updateExpenseDto.Value;

                foreach (var expenseTotal in expensesTotal)
                {
                    if (expenseTotal.Date < DateTime.UtcNow)
                        continue;

                    await _expenseTotalService.DecreaseFromTotal(newValue, expenseTotal);
                }
            }

            existingExpense.Value = updateExpenseDto.Value;

            await _expenseRepository.UpdateExpenseAsync(existingExpense);
        }
    }
}
