using Core.Entities;
using Core.Infrastructure;

namespace CoreTests.Services.ExpenseServiceTests.Methods
{
    public class UpdateAsyncTests : ExpenseServiceTests
    {
        [Fact]
        public async Task When_ExpenseDoesNotExist_Then_ThrowsException()
        {
            #region Arrange
            var expenseId = Guid.NewGuid();

            _expenseRepository
                .GetByIdAsync(expenseId)
                .Returns(default(Expense));
            #endregion

            // Act
            var exception = await Record.ExceptionAsync(async () =>
                await _sut.UpdateAsync(default, expenseId));

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task When_ExpenseDtoHasTheSameTitleAndValueOfExpenseEntity_Then_DoesNotUpdate()
        {
            #region Arrange
            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                Title = "Existing title",
                Value = 1.0
            };

            var updateExpenseDto = new UpdateExpenseDto(expense.Title, expense.Value, default);

            _expenseRepository
                .GetByIdAsync(expense.Id)
                .Returns(default(Expense));
            #endregion

            // Act
            var exception = await Record.ExceptionAsync(async () =>
                await _sut.UpdateAsync(updateExpenseDto, expense.Id));

            // Assert
            await _expenseRepository
                .DidNotReceiveWithAnyArgs()
                .UpdateExpenseAsync(default);

            await _expenseTotalService
                .DidNotReceiveWithAnyArgs()
                .AddToTotal(default, default);

            await _expenseTotalService
                .DidNotReceiveWithAnyArgs()
                .DecreaseFromTotal(default, default);
        }
    }
}
