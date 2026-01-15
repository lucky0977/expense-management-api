using ExpenseManagement.Domain.Entities;

namespace ExpenseManagement.Application.Interfaces
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> GetAllByUserIdAsync(int userId);
        Task<Expense?> GetByIdAndUserIdAsync(int id, int userId);

        Task AddAsync(Expense expense);
        Task UpdateAsync(Expense expense);
        Task DeleteAsync(Expense expense);

        Task<List<Expense>> GetByMonthAsync(int month, int year, int userId);
        Task<List<Expense>> GetByDateRangeAsync(DateTime from, DateTime to, int userId);
    }
}
