namespace CoreTests.Services.ExpenseServiceTests.Methods
{
    public class GetAllAsyncTests : ExpenseServiceTests
    {
        [Fact]
        public async void When_Invoked_Then_GetAllExpensesFromExpenseRepository()
        {
            // Act
            await _sut.GetAllAsync();

            // Assert
            await _expenseRepository
                .Received()
                .GetAllAsync();
        }
    }
}
