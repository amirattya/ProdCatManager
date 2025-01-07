using System.ComponentModel.DataAnnotations;

namespace ProdCatManager.Dtos.Categories
{
    public class CategoryRequestDto
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public Guid? ParentCategoryId { get; set; }
        [Required]
        public string Status { get; set; } // Enum as string
    }
}