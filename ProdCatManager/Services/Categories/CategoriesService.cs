using ProdCatManager.Dtos.Categories;
using ProdCatManager.Dtos.Products;
using ProdCatManager.Models;
using ProdCatManager.Repositories;

namespace ProdCatManager.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _repository;

        public CategoriesService(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Status = c.Status.ToString(),
                ParentCategoryId = c.ParentCategoryId,
                ParentCategoryName = c.ParentCategory?.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate
            });
        }

        public async Task<CategoryResponseDto?> GetByIdAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Status = category.Status.ToString(),
                ParentCategoryId = category.ParentCategoryId,
                ParentCategoryName = category.ParentCategory?.Name,
                CreatedDate = category.CreatedDate,
                UpdatedDate = category.UpdatedDate
            };
        }

        public async Task AddAsync(CategoryRequestDto request)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ParentCategoryId = request.ParentCategoryId,
                Status = Enum.Parse<CategoryStatus>(request.Status),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _repository.AddAsync(category);
        }

        public async Task<bool> UpdateAsync(Guid id, CategoryRequestDto request)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return false;

            category.Name = request.Name;
            category.Description = request.Description;
            category.ParentCategoryId = request.ParentCategoryId;
            category.Status = Enum.Parse<CategoryStatus>(request.Status);
            category.UpdatedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(category);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return false;

            if (await _repository.HasProductsAsync(id))
                return false; // Prevent deletion if products exist.

            await _repository.DeleteAsync(category);
            return true;
        }
    }
}