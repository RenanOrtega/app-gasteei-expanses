using Core.Entities.Interfaces;
using Core.Models;

namespace Core.Entities
{
    public class Expense : IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public double Value { get; set; }
        public ExpenseRecorrence Type { get; set; }
        public ExpenseStartDate Start { get; set; }
        public ExpenseEndDate End { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
