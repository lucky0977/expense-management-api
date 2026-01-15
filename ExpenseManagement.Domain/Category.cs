using ExpenseManagement.Domain.Entities;

namespace ExpenseManagement.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
