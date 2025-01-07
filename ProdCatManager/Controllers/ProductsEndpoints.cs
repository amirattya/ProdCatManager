using ProdCatManager.Dtos.Products;
using ProdCatManager.Services;

namespace ProdCatManager.Controllers
{
    public static class ProductsEndpoints
    {
        public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            var productsGroup = app.MapGroup("/api/products").WithTags("Products");

            // Get All Products (with Pagination, Filtering, Sorting)
            productsGroup.MapGet("/", async (IProductsService service, [AsParameters] PaginatedRequestDto request) =>
            {
                var (products, total) = await service.GetProductsAsync(request);
                return Results.Ok(new { Total = total, Data = products });
            });

            // Get a Product by ID
            productsGroup.MapGet("/{id:guid}", async (IProductsService service, Guid id) =>
            {
                var product = await service.GetProductByIdAsync(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            });

            // Create a New Product
            productsGroup.MapPost("/", async (IProductsService service, ProductRequestDto productDto) =>
            {
                await service.CreateProductAsync(productDto);
                return Results.Created($"/api/products/{productDto.Name}", productDto);
            });

            // Update a Product
            productsGroup.MapPut("/{id:guid}", async (IProductsService service, Guid id, ProductRequestDto productDto) =>
            {
                var result = await service.UpdateProductAsync(id, productDto);
                return result ? Results.Ok() : Results.NotFound();
            });

            // Delete a Product
            productsGroup.MapDelete("/{id:guid}", async (IProductsService service, Guid id) =>
            {
                var result = await service.DeleteProductAsync(id);
                return result ? Results.NoContent() : Results.NotFound();
            });

            return app;
        }
    }
}
