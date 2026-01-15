namespace ExpenseManagement.Domain.Entities;

public class Expense
{
    public int Id { get; set; }

    public int UserId { get; set; }   // ✅ NO default string

    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }

    public Category Category { get; set; } = null!;
}
