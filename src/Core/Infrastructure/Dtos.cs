using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.Infrastructure
{
    public record ExpenseDto(Guid Id, string Email, string Title, double Value, ExpenseRecorrence Type, ExpenseStartDate? Start, ExpenseEndDate? End, DateTimeOffset Created);
    public record CreateExpenseDto([Required] string Title, [Required] string Email, [Required] double Value, [Required] ExpenseRecorrence Type, ExpenseStartDate Start, ExpenseEndDate End);
    public record UpdateExpenseDto([Required] string Title, [Required] double Value, [Required] ExpenseRecorrence Type);

    public record ExpenseTotalDto(Guid Id, string Email, DateTime Date, double Total, DateTimeOffset Created);
    public record CreateExpenseTotalDto([Required] string Email, [Required] DateTime Date, [Required] double Total);

}
