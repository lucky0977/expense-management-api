namespace ExpenseManagement.Application.DTOs.Admin
{
    public class DashboardSummaryDto
    {
        public int TotalUsers { get; set; }
        public int TotalExpenses { get; set; }
        public decimal TotalExpenseAmount { get; set; }
        public List<MonthlyExpenseDto> MonthlyExpenses { get; set; } = [];
        public List<CategoryExpenseDto> CategoryExpenses { get; set; } = [];
    }
}
