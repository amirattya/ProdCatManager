namespace ProdCatManager.Dtos.Dashboard
{
    public class LowStockAlertDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int StockQuantity { get; set; }
    }
}