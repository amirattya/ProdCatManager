using Microsoft.EntityFrameworkCore;
using ProdCatManager.Dtos.Products;
using ProdCatManager.Data;
using ProdCatManager.Models;

namespace ProdCatManager.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Product>, int)> GetProductsAsync(PaginatedRequestDto request)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(request.Filter))
                query = query.Where(p => p.Name.Contains(request.Filter));
            if (request.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == request.CategoryId);
            if (!string.IsNullOrEmpty(request.Status))
                query = query.Where(p => p.Status.ToString() == request.Status);

            // Sorting
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                query = request.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(p => EF.Property<object>(p, request.SortBy))
                    : query.OrderBy(p => EF.Property<object>(p, request.SortBy));
            }

            // Pagination
            var totalItems = await query.CountAsync();
            var products = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return (products, totalItems);
        }

        public async Task<Product?> GetProductByIdAsync(Guid id) =>
            await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
