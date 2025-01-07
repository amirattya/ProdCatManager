using ProdCatManager.Dtos.Products;

namespace ProdCatManager.Services
{
    public interface IProductsService
    {
        Task<(IEnumerable<ProductResponseDto>, int)> GetProductsAsync(PaginatedRequestDto request);
        Task<ProductResponseDto?> GetProductByIdAsync(Guid id);
        Task CreateProductAsync(ProductRequestDto productDto);
        Task<bool> UpdateProductAsync(Guid id, ProductRequestDto productDto);
        Task<bool> DeleteProductAsync(Guid id);
    }
}