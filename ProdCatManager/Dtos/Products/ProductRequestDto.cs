using System.ComponentModel.DataAnnotations;

namespace ProdCatManager.Dtos.Products
{
    public class ProductRequestDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(500)]
        public string Description { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public string Status { get; set; } // Enum as string

        [Required, Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public string? ImageUrl { get; set; }
    }
}
