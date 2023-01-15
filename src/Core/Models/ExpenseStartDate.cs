using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ExpenseStartDate
    {
        public DateTime Date { get; set; }

        public ExpenseStartDate(DateTime date)
        {
            Date = date;
        }
    }
}
