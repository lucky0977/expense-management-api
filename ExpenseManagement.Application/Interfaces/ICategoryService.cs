using ExpenseManagement.Application.DTOs.Category;
using ExpenseManagement.Domain.Entities;

namespace ExpenseManagement.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> CreateAsync(string name);

        // 🆕 ADD THESE
        Task<Category> UpdateAsync(int id, UpdateCategoryRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
