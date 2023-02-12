using Core.Infrastructure.Repositories.Interfaces;
using Core.Services;
using Core.Services.Interfaces;

namespace CoreTests.Services.ExpenseServiceTests
{
    public abstract class ExpenseServiceTests
    {
        protected readonly IExpenseRepository _expenseRepository;
        protected readonly IExpenseTotalService _expenseTotalService;

        protected readonly IExpenseService _sut;

        public ExpenseServiceTests()
        {
            _expenseRepository = Substitute.For<IExpenseRepository>();
            _expenseTotalService = Substitute.For<IExpenseTotalService>();

            _sut = new ExpenseService(_expenseRepository, _expenseTotalService);
        }
    }
}
