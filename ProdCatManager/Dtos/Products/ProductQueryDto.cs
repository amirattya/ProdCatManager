namespace ProdCatManager.Dtos.Products
{
    public class ProductQueryDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; } // "asc" or "desc"
        public string? Filter { get; set; }
        public Guid? CategoryId { get; set; }
        public string? Status { get; set; }
    }
}
