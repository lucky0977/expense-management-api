using ExpenseManagement.Application.DTOs.Admin;

namespace ExpenseManagement.Application.Interfaces
{
    public interface IAdminService
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync();
    }
}
