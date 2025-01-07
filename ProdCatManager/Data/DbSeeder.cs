using ProdCatManager.Models;

namespace ProdCatManager.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                // Seed Categories
                var electronics = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Electronics",
                    Description = "Devices and gadgets",
                    Status = CategoryStatus.Active,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                var clothing = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Clothing",
                    Description = "Apparel and garments",
                    Status = CategoryStatus.Active,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                context.Categories.AddRange(electronics, clothing);
                await context.SaveChangesAsync();

                // Seed Products
                var products = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Smartphone",
                        Description = "Latest model smartphone",
                        Price = 699.99m,
                        CategoryId = electronics.Id,
                        Status = ProductStatus.Active,
                        StockQuantity = 50,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "T-Shirt",
                        Description = "Cotton t-shirt",
                        Price = 19.99m,
                        CategoryId = clothing.Id,
                        Status = ProductStatus.Active,
                        StockQuantity = 200,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    }
                };

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
