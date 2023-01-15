namespace Core.Models
{
    public class ExpenseEndDate
    {
        public ExpenseEndDate(DateTime date)
        {
            Date = date;
        }
        public DateTime Date { get; set; }
    }
}
