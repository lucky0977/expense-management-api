
using ExpenseManagement.Application.DTOs.Expenses;
using ExpenseManagement.Domain.Entities;

namespace ExpenseManagement.Application.Interfaces
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetAllAsync(int userId);
        Task<Expense?> GetByIdAsync(int id, int userId);
        Task<Expense> CreateAsync(CreateExpenseRequest request, int userId);
        Task<bool> UpdateAsync(int id, CreateExpenseRequest request, int userId);
        Task<bool> DeleteAsync(int id, int userId);

        // 🆕 FILTERS
        Task<List<Expense>> GetByMonthAsync(int month, int year, int userId);
        Task<List<Expense>> GetByDateRangeAsync(DateTime from, DateTime to, int userId);
    }
}
