using Microsoft.EntityFrameworkCore;
using ProdCatManager.Dtos.Products;
using ProdCatManager.Data;
using ProdCatManager.Models;

namespace ProdCatManager.Repositories
{
    public interface IProductsRepository
    {
        Task<(IEnumerable<Product>, int)> GetProductsAsync(PaginatedRequestDto request);
        Task<Product?> GetProductByIdAsync(Guid id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
    }
}
