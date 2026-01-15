namespace ExpenseManagement.Application.DTOs.Admin
{
    public class CategoryExpenseDto
    {
        public string CategoryName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }
}
