using Core.Entities;

namespace CoreTests.Services.ExpenseServiceTests.Methods
{
    public class DeleteExpenseAsyncTests : ExpenseServiceTests
    {
        [Fact]
        public async Task When_ExpenseIsRecurrent_Then_ExpenseValueIsDecreasedForAllRecurrentMonths()
        {
            #region Arrange
            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                Email = "user@unit.test.com",
                Start = new(DateTime.UtcNow),
                End = new(DateTime.UtcNow),
                Value = 1.0
            };

            var expenseTotals = new List<ExpenseTotal>
            {
                new() { Id = Guid.NewGuid() },
                new() { Id = Guid.NewGuid() },
                new() { Id = Guid.NewGuid() }
            };

            _expenseRepository
                .GetByIdAsync(expense.Id)
                .Returns(expense);

            _expenseTotalService
                .GetAllExpensesTotal(expense.Email, Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(expenseTotals);
            #endregion

            // Act
            await _sut.DeleteExpenseAsync(expense.Id);

            // Assert
            foreach (var expenseTotal in expenseTotals)
            {
                await _expenseTotalService
                    .Received()
                    .DecreaseFromTotal(expense.Value, expenseTotal);
            }
        }
    }
}
