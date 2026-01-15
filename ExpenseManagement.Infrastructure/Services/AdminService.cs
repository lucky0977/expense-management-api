using ExpenseManagement.Application.DTOs.Admin;
using ExpenseManagement.Application.Interfaces;
using ExpenseManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;

        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
        {
            var totalUsers = await _context.Users.CountAsync();
            var totalExpenses = await _context.Expenses.CountAsync();
            var totalExpenseAmount = await _context.Expenses.SumAsync(e => e.Amount);

            var monthlyExpenses = await _context.Expenses
                .GroupBy(e => new { e.CreatedAt.Year, e.CreatedAt.Month })
                .Select(g => new MonthlyExpenseDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalAmount = g.Sum(x => x.Amount)
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            var categoryExpenses = await _context.Expenses
                .Include(e => e.Category)
                .GroupBy(e => e.Category.Name)
                .Select(g => new CategoryExpenseDto
                {
                    CategoryName = g.Key,
                    TotalAmount = g.Sum(x => x.Amount)
                })
                .ToListAsync();

            return new DashboardSummaryDto
            {
                TotalUsers = totalUsers,
                TotalExpenses = totalExpenses,
                TotalExpenseAmount = totalExpenseAmount,
                MonthlyExpenses = monthlyExpenses,
                CategoryExpenses = categoryExpenses
            };
        }
    }
}
