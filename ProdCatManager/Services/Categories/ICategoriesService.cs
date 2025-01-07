using ProdCatManager.Dtos.Categories;

namespace ProdCatManager.Services
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
        Task<CategoryResponseDto?> GetByIdAsync(Guid id);
        Task AddAsync(CategoryRequestDto request);
        Task<bool> UpdateAsync(Guid id, CategoryRequestDto request);
        Task<bool> DeleteAsync(Guid id);
    }
}