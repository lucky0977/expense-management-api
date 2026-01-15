namespace ExpenseManagement.Application.DTOs.Admin
{
    public class MonthlyExpenseDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
