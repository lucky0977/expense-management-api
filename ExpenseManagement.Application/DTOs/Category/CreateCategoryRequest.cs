using System.ComponentModel.DataAnnotations;

namespace ExpenseManagement.Application.DTOs.Category
{
    public class CreateCategoryRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
