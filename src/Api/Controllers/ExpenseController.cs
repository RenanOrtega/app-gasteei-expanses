using Core.Entities;
using Core.Infrastructure;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // GET v1/<ExpenseController>
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAllAsync()
        {
            var expenses = await _expenseService.GetAllAsync();
            return Ok(expenses);
        }

        // GET v1/<ExpenseController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseDto>> GetByIdAsync([FromRoute] Guid id)
        {
            var expense = await _expenseService.GetByIdAsync(id);
            return Ok(expense);
        }

        // POST v1/<ExpenseController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createExpenseDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ExpenseDto>> PostAsync([FromBody] CreateExpenseDto createExpenseDto)
        {
            var expense = new Expense()
            {
                Email = createExpenseDto.Email,
                Title = createExpenseDto.Title,
                Value = createExpenseDto.Value,
                Type = createExpenseDto.Type,
                Start = createExpenseDto.Start,
                End = createExpenseDto.End,
                Created = DateTimeOffset.Now
            };

            await _expenseService.CreateAsync(expense);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = expense.Id }, expense);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] UpdateExpenseDto updateExpenseDto, [FromRoute] Guid id)
        {
            await _expenseService.UpdateAsync(updateExpenseDto, id);
            return NoContent();
        }
    }
}
