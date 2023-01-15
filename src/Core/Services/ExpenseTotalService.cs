using Core.Entities;
using Core.Infrastructure;
using Core.Infrastructure.Repositories.Interfaces;
using Core.Services.Interfaces;
using MongoDB.Driver;

namespace Core.Services
{
    public class ExpenseTotalService : IExpenseTotalService
    {
        private readonly IExpenseTotalRepository _expenseTotalRepository;

        public ExpenseTotalService(IExpenseTotalRepository expenseTotalRepository)
        {
            _expenseTotalRepository = expenseTotalRepository;
        }

        public async Task<ExpenseTotalDto> GetAsync(Guid id)
        {
            var expenseTotal = await _expenseTotalRepository.GetByIdAsync(id);

            if (expenseTotal is null)
                throw new Exception($"Expense total with Id = {id} not found");

            return expenseTotal.AsDto();
        }

        public async Task<IEnumerable<ExpenseTotalDto>> GetAllAsync()
        {
            return (await _expenseTotalRepository.GetAllAsync()).Select(expenseTotal => expenseTotal.AsDto());
        }

        public async Task<IEnumerable<ExpenseTotal>> GetAllExpensesTotal(string email, DateTime startDate, DateTime endDate)
        {
            var filter = CreateFilterByEmailAndDates(email, startDate, endDate);
            return await _expenseTotalRepository.GetAllByFilterAsync(filter);
        }

        public async Task<ExpenseTotal> GetExpenseTotal(string email, DateTime startDate, DateTime endDate)
        {
            var filter = CreateFilterByEmailAndDates(email, startDate, endDate);
            return await _expenseTotalRepository.GetByFilterAsync(filter);
        }

        public async Task<ExpenseTotal> GetExpenseTotal(string email, DateTime date)
        {
            var filterBuilder = Builders<ExpenseTotal>.Filter;
            var filter = filterBuilder.Eq(x => x.Email, email)
                        & filterBuilder.Eq(x => x.Date, new DateTime(date.Year, date.Month, 1));

            return await _expenseTotalRepository.GetByFilterAsync(filter);
        }

        public static IList<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            IList<DateTime> allDates = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddMonths(1))
            {
                allDates.Add(date);
            }

            return allDates;
        }

        public async Task FirstExpenseTotalCreation(ExpenseTotal expenseTotal)
        {
            await _expenseTotalRepository.CreateAsync(expenseTotal);
        }

        public async Task CreateExpenseTotalIfNotExists(Expense expense)
        {
            if (expense.Start.Date == expense.End.Date)
            {
                await CreateSingleMonth(expense);
                return;
            }

            var dates = GetDatesBetween(expense.Start.Date, expense.End.Date);

            foreach (var date in dates)
            {
                var expenseTotal = await GetExpenseTotal(expense.Email, date, date);

                if (expenseTotal is null)
                {
                    var newExpenseTotal = new ExpenseTotal()
                    {
                        Date = new DateTime(date.Year, date.Month, 1),
                        Email = expense.Email,
                        Total = 0,
                        Created = DateTimeOffset.Now,
                    };

                    await _expenseTotalRepository.CreateAsync(newExpenseTotal);
                }
            }
        }

        private async Task CreateSingleMonth(Expense expense)
        {
            var expenseTotal = await GetExpenseTotal(expense.Email, expense.End.Date);

            if (expenseTotal is null)
            {
                var newExpenseTotal = new ExpenseTotal()
                {
                    Date = new DateTime(expense.End.Date.Year, expense.End.Date.Month, 1),
                    Email = expense.Email,
                    Total = 0,
                    Created = DateTimeOffset.Now,
                };
                await _expenseTotalRepository.CreateAsync(newExpenseTotal);
            }
        }

        public async Task AddToTotal(double newValue, ExpenseTotal expenseTotal)
        {
            expenseTotal.Total += newValue;
            await _expenseTotalRepository.UpdateExpenseAsync(expenseTotal);
        }

        public async Task DecreaseFromTotal(double newValue, ExpenseTotal expenseTotal)
        {
            expenseTotal.Total -= newValue;
            await _expenseTotalRepository.UpdateExpenseAsync(expenseTotal);
        }

        private static FilterDefinition<ExpenseTotal> CreateFilterByEmailAndDates(string email, DateTime startDate, DateTime endDate)
        {
            var filterBuilder = Builders<ExpenseTotal>.Filter;
            var filter = filterBuilder.Eq(x => x.Email, email)
                        & filterBuilder.Gte(x => x.Date, new DateTime(startDate.Year, startDate.Month, 1))
                        & filterBuilder.Lte(x => x.Date, new DateTime(endDate.Year, endDate.Month, 1));

            return filter;
        }
    }
}
