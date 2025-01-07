using ProdCatManager.Dtos.Products;
using ProdCatManager.Models;

namespace ProdCatManager.Repositories
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<bool> HasProductsAsync(Guid categoryId);
    }
}
