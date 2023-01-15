using Core.Entities.Interfaces;

namespace Core.Entities
{
    public class ExpenseTotal : IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
