using Core.Entities;

namespace CoreTests.Services.ExpenseServiceTests.Methods
{
    public class GetByIdAsyncTests : ExpenseServiceTests
    {
        [Fact]
        public async Task When_ExpenseExists_Then_GetExpenseFromRepository()
        {
            #region Arrange
            var expense = new Expense { Id = Guid.NewGuid() };

            _expenseRepository
                .GetByIdAsync(expense.Id)
                .Returns(expense);
            #endregion

            // Act
            var result = await _sut.GetByIdAsync(expense.Id);

            // Assert
            Assert.Equal(expense.Id, result.Id);
        }

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
                await _sut.GetByIdAsync(expenseId));

            // Assert
            Assert.NotNull(exception);
        }
    }
}
