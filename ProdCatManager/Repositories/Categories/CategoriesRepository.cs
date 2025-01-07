using Microsoft.EntityFrameworkCore;
using ProdCatManager.Data;
using ProdCatManager.Models;

namespace ProdCatManager.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await _context.Categories.Include(c => c.ParentCategory).ToListAsync();

        public async Task<Category?> GetByIdAsync(Guid id) =>
            await _context.Categories.Include(c => c.ParentCategory).FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasProductsAsync(Guid categoryId) =>
            await _context.Products.AnyAsync(p => p.CategoryId == categoryId);
    }
}