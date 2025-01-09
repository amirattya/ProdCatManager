using Microsoft.EntityFrameworkCore;
using ProdCatManager.Data;
using ProdCatManager.Dtos.Dashboard;
using ProdCatManager.Services;
using ProdCatManager.Data;
using ProdCatManager.Dtos.Dashboard;

namespace ProdCatManager.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<KeyMetricsDto> GetKeyMetricsAsync()
        {
            var totalProducts = await _context.Products.CountAsync();
            var totalCategories = await _context.Categories.CountAsync();

            var productsPerCategory = await _context.Products
                .GroupBy(p => p.Category.Name)
                .Select(g => new { CategoryName = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.CategoryName, x => x.Count);

            return new KeyMetricsDto
            {
                TotalProducts = totalProducts,
                TotalCategories = totalCategories,
                ProductsPerCategory = productsPerCategory
            };
        }

        public async Task<IEnumerable<LowStockAlertDto>> GetLowStockAlertsAsync(int threshold)
        {
            return await _context.Products
                .Where(p => p.StockQuantity < threshold)
                .Select(p => new LowStockAlertDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    StockQuantity = p.StockQuantity
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ChartDataDto>> GetProductsByStatusAsync()
        {
            return await _context.Products
                .GroupBy(p => p.Status)
                .Select(g => new ChartDataDto
                {
                    Label = g.Key.ToString(),
                    Value = g.Count()
                })
                .ToListAsync();
        }
    }
}
