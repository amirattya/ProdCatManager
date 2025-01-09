using ProdCatManager.Dtos.Dashboard;

namespace ProdCatManager.Services
{
    public interface IDashboardService
    {
        Task<KeyMetricsDto> GetKeyMetricsAsync();
        Task<IEnumerable<LowStockAlertDto>> GetLowStockAlertsAsync(int threshold);
        Task<IEnumerable<ChartDataDto>> GetProductsByStatusAsync();
    }
}