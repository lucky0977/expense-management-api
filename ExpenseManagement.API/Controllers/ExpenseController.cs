using ExpenseManagement.Application.DTOs.Expenses;
using ExpenseManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // GET: api/Expense
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var expenses = await _expenseService.GetAllAsync(userId);
            return Ok(expenses);
        }

        // GET: api/Expense/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var expense = await _expenseService.GetByIdAsync(id, userId);

            if (expense == null)
                return NotFound();

            return Ok(expense);
        }

        // POST: api/Expense
        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var expense = await _expenseService.CreateAsync(request, userId);
            return Ok(expense);
        }

        // PUT: api/Expense/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateExpenseRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var updated = await _expenseService.UpdateAsync(id, request, userId);

            if (!updated)
                return NotFound();

            return Ok();
        }

        // DELETE: api/Expense/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var deleted = await _expenseService.DeleteAsync(id, userId);

            if (!deleted)
                return NotFound();

            return Ok();
        }

        // ✅ FILTER BY MONTH
        // GET: api/Expense/month?month=1&year=2026
        [HttpGet("month")]
        public async Task<IActionResult> GetByMonth(int month, int year)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var expenses = await _expenseService.GetByMonthAsync(month, year, userId);
            return Ok(expenses);
        }


        // ✅ FILTER BY DATE RANGE
        // GET: api/Expense/range?from=2026-01-01&to=2026-01-31
        [HttpGet("range")]
        public async Task<IActionResult> GetByDateRange(DateTime from, DateTime to)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var expenses = await _expenseService.GetByDateRangeAsync(from, to, userId);
            return Ok(expenses);
        }
    }
}
