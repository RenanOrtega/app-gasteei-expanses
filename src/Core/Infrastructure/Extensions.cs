using Core.Entities;

namespace Core.Infrastructure
{
    public static class Extensions
    {
        public static ExpenseDto AsDto(this Expense expense)
        {
            return new ExpenseDto(expense.Id, expense.Email, expense.Title, expense.Value, expense.Type, expense.Start, expense.End, expense.Created);
        }

        public static ExpenseTotalDto AsDto(this ExpenseTotal expenseTotal)
        {
            return new ExpenseTotalDto(expenseTotal.Id, expenseTotal.Email, expenseTotal.Date, expenseTotal.Total, expenseTotal.Created);
        }
    }
}
