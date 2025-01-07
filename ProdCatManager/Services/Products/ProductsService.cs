using ProdCatManager.Dtos.Products;
using ProdCatManager.Models;
using ProdCatManager.Repositories;

namespace ProdCatManager.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _repository;

        public ProductsService(IProductsRepository repository)
        {
            _repository = repository;
        }

        public async Task<(IEnumerable<ProductResponseDto>, int)> GetProductsAsync(PaginatedRequestDto request)
        {
            var (products, totalItems) = await _repository.GetProductsAsync(request);
            var productDtos = products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Status = p.Status.ToString(),
                StockQuantity = p.StockQuantity,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category.Name,
                CreatedDate = p.CreatedDate,
                UpdatedDate = p.UpdatedDate
            });

            return (productDtos, totalItems);
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null) return null;

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Status = product.Status.ToString(),
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                CategoryName = product.Category.Name,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedDate
            };
        }

        public async Task CreateProductAsync(ProductRequestDto productDto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Status = Enum.Parse<ProductStatus>(productDto.Status),
                StockQuantity = productDto.StockQuantity,
                CategoryId = productDto.CategoryId,
                ImageUrl = productDto.ImageUrl,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _repository.AddProductAsync(product);
        }

        public async Task<bool> UpdateProductAsync(Guid id, ProductRequestDto productDto)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null) return false;

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.Status = Enum.Parse<ProductStatus>(productDto.Status);
            product.StockQuantity = productDto.StockQuantity;
            product.CategoryId = productDto.CategoryId;
            product.ImageUrl = productDto.ImageUrl;
            product.UpdatedDate = DateTime.UtcNow;

            await _repository.UpdateProductAsync(product);
            return true;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null) return false;

            await _repository.DeleteProductAsync(product);
            return true;
        }
    }
}