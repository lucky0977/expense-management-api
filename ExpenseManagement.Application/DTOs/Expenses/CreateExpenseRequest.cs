namespace ExpenseManagement.Application.DTOs.Expenses
{
    public class CreateExpenseRequest
    {
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
