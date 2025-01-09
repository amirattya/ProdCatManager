namespace ProdCatManager.Dtos.Dashboard
{
    public class KeyMetricsDto
    {
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public Dictionary<string, int> ProductsPerCategory { get; set; }
    }
}