using Core.Entities;

namespace CoreTests.Services.ExpenseServiceTests.Methods
{
    public class CreateAsyncTests : ExpenseServiceTests
    {
        [Fact]
        public async Task When_ExpenseIsRecurrent_Then_ExpenseValueIsAddedForAllRecurrentMonths()
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

            _expenseTotalService
                .GetAllExpensesTotal(expense.Email, Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(expenseTotals);
            #endregion

            // Act
            await _sut.CreateAsync(expense);

            // Assert
            foreach (var expenseTotal in expenseTotals)
            {
                await _expenseTotalService
                    .Received()
                    .AddToTotal(expense.Value, expenseTotal);
            }
        }
    }
}
