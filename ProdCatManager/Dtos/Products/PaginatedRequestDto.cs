namespace ProdCatManager.Dtos.Products
{
    public class PaginatedRequestDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; } // e.g., "name", "price"
        public string? SortOrder { get; set; } // "asc" or "desc"
        public string? Filter { get; set; } // e.g., "laptop"
        public Guid? CategoryId { get; set; }
        public string? Status { get; set; } // Enum as string
    }
}
