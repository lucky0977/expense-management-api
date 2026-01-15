using ExpenseManagement.Application.DTOs.Category;
using ExpenseManagement.Application.Interfaces;
using ExpenseManagement.Domain.Entities;

namespace ExpenseManagement.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Category> CreateAsync(string name)
        {
            var category = new Category
            {
                Name = name
            };

            await _repository.AddAsync(category);
            return category;
        }

        // 🆕 UPDATE
        public async Task<Category> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new Exception("Category not found");

            category.Name = request.Name;
            await _repository.UpdateAsync(category);

            return category;
        }

        // 🆕 DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return false;

            await _repository.DeleteAsync(category);
            return true;
        }
    }
}
