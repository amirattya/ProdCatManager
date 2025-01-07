using ProdCatManager.Dtos.Categories;
using ProdCatManager.Services;

namespace ProdCatManager.Controllers
{
    public static class CategoriesEndpoints
    {
        public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder app)
        {
            var categoriesGroup = app.MapGroup("/api/categories").WithTags("Categories");

            categoriesGroup.MapGet("/", async (ICategoriesService service) =>
            {
                var categories = await service.GetAllAsync();
                return Results.Ok(categories);
            });

            categoriesGroup.MapGet("/{id:guid}", async (ICategoriesService service, Guid id) =>
            {
                var category = await service.GetByIdAsync(id);
                return category is not null ? Results.Ok(category) : Results.NotFound();
            });

            categoriesGroup.MapPost("/", async (ICategoriesService service, CategoryRequestDto request) =>
            {
                await service.AddAsync(request);
                return Results.Created($"/api/categories/{request.Name}", request);
            });

            categoriesGroup.MapPut("/{id:guid}", async (ICategoriesService service, Guid id, CategoryRequestDto request) =>
            {
                var result = await service.UpdateAsync(id, request);
                return result ? Results.Ok() : Results.NotFound();
            });

            categoriesGroup.MapDelete("/{id:guid}", async (ICategoriesService service, Guid id) =>
            {
                var result = await service.DeleteAsync(id);
                return result ? Results.NoContent() : Results.BadRequest("Cannot delete category with products.");
            });

            return app;
        }
    }
}