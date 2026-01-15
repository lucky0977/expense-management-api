using ExpenseManagement.Application.Interfaces;
using ExpenseManagement.Domain.Entities;
using ExpenseManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.Infrastructure.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Expense>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public async Task<Expense?> GetByIdAndUserIdAsync(int id, int userId)
        {
            return await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        }

        public async Task AddAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Expense expense)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Expense>> GetByMonthAsync(int month, int year, int userId)
        {
            return await _context.Expenses
                .Where(e =>
                    e.UserId == userId &&
                    e.CreatedAt.Month == month &&
                    e.CreatedAt.Year == year)
                .ToListAsync();
        }

        public async Task<List<Expense>> GetByDateRangeAsync(DateTime from, DateTime to, int userId)
        {
            return await _context.Expenses
                .Where(e =>
                    e.UserId == userId &&
                    e.CreatedAt >= from &&
                    e.CreatedAt <= to)
                .ToListAsync();
        }
    }
}
