using ExpenseManagement.Application.DTOs.Expenses;
using ExpenseManagement.Application.Interfaces;
using ExpenseManagement.Domain.Entities;

namespace ExpenseManagement.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        // ✅ GET ALL EXPENSES FOR USER
        public async Task<List<Expense>> GetAllAsync(int userId)
        {
            return await _expenseRepository.GetAllByUserIdAsync(userId);
        }

        // ✅ GET EXPENSE BY ID (USER-SCOPED)
        public async Task<Expense?> GetByIdAsync(int id, int userId)
        {
            return await _expenseRepository.GetByIdAndUserIdAsync(id, userId);
        }

        // ✅ CREATE EXPENSE
        public async Task<Expense> CreateAsync(CreateExpenseRequest request, int userId)
        {
            var expense = new Expense
            {
                Title = request.Title,
                Amount = request.Amount,
                CategoryId = request.CategoryId,
                CreatedAt = request.CreatedAt,
                UserId = userId
            };

            await _expenseRepository.AddAsync(expense);
            return expense;
        }

        // ✅ UPDATE EXPENSE
        public async Task<bool> UpdateAsync(int id, CreateExpenseRequest request, int userId)
        {
            var expense = await _expenseRepository.GetByIdAndUserIdAsync(id, userId);
            if (expense == null)
                return false;

            expense.Title = request.Title;
            expense.Amount = request.Amount;
            expense.CategoryId = request.CategoryId;
            expense.CreatedAt = request.CreatedAt;

            await _expenseRepository.UpdateAsync(expense);
            return true;
        }

        // ✅ DELETE EXPENSE
        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var expense = await _expenseRepository.GetByIdAndUserIdAsync(id, userId);
            if (expense == null)
                return false;

            await _expenseRepository.DeleteAsync(expense);
            return true;
        }

        // ✅ FILTER BY MONTH
        public async Task<List<Expense>> GetByMonthAsync(int month, int year, int userId)
        {
            if (month < 1 || month > 12)
                throw new ArgumentException("Invalid month");

            return await _expenseRepository.GetByMonthAsync(month, year, userId);
        }

        // ✅ FILTER BY DATE RANGE
        public async Task<List<Expense>> GetByDateRangeAsync(DateTime from, DateTime to, int userId)
        {
            if (from > to)
                throw new ArgumentException("From date cannot be after To date");

            return await _expenseRepository.GetByDateRangeAsync(from, to, userId);
        }
    }
}
