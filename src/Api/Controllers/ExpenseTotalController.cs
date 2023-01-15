using Core.Entities;
using Core.Infrastructure;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ExpenseTotalController : ControllerBase
    {
        private readonly IExpenseTotalService _expenseTotalService;

        public ExpenseTotalController(IExpenseTotalService expenseTotalService)
        {
            _expenseTotalService = expenseTotalService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseTotalDto>> GetByIdAsync(Guid id)
        {
            var expenseTotal = await _expenseTotalService.GetAsync(id);
            return Ok(expenseTotal);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseTotalDto>>> GetAllAsync()
        {
            var expensesTotal = await _expenseTotalService.GetAllAsync();
            return Ok(expensesTotal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createExpenseTotalDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ExpenseTotalDto>> PostAsync([FromBody] CreateExpenseTotalDto createExpenseTotalDto)
        {
            var date = new DateTime(createExpenseTotalDto.Date.Year, createExpenseTotalDto.Date.Month, 1);

            var expenseTotal = new ExpenseTotal
            {
                Date = date,
                Email = createExpenseTotalDto.Email,
                Total = createExpenseTotalDto.Total,
                Created = DateTimeOffset.Now
            };

            await _expenseTotalService.FirstExpenseTotalCreation(expenseTotal);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = expenseTotal.Id }, expenseTotal);
        }
    }
}
