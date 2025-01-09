using ProdCatManager.Dtos.Dashboard;
using ProdCatManager.Services;

namespace ProdCatManager.Controllers
{
    public static class DashboardEndpoints
    {
        public static IEndpointRouteBuilder MapDashboardEndpoints(this IEndpointRouteBuilder app)
        {
            var dashboardGroup = app.MapGroup("/api/dashboard").WithTags("Dashboard");

            dashboardGroup.MapGet("/key-metrics", async (IDashboardService service) =>
            {
                var metrics = await service.GetKeyMetricsAsync();
                return Results.Ok(metrics);
            });

            dashboardGroup.MapGet("/low-stock-alerts", async (IDashboardService service, int threshold) =>
            {
                var alerts = await service.GetLowStockAlertsAsync(threshold);
                return Results.Ok(alerts);
            });

            dashboardGroup.MapGet("/products-by-status", async (IDashboardService service) =>
            {
                var chartData = await service.GetProductsByStatusAsync();
                return Results.Ok(chartData);
            });

            return app;
        }
    }
}