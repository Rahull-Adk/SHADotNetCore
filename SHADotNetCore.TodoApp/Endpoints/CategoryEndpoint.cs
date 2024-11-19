using Microsoft.EntityFrameworkCore;
using SHADotNetCore.TodoApp.Model;
using System.Runtime.CompilerServices;

namespace SHADotNetCore.TodoApp.Endpoints
{
    public static class CategoryEndpoint
    {
        public static IEndpointRouteBuilder UseCategoryEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/category", async (DataContext db) =>
            {
                var categories = await db.Categories.Where(c => !c.DeleteFlag).ToListAsync();
                return Results.Ok(categories);
            }).WithName("GetCategories").WithOpenApi();

            app.MapGet("/category/{id}", async (int id, DataContext db) =>
            {
                var category = await db.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
                if (category is null)
                {
                    return Results.NotFound("No Category Found");
                }
                return Results.Ok(category);
            }).WithName("GetCategory").WithOpenApi();

            app.MapPost("/category", async (Category category, DataContext db) =>
            {
                await db.Categories.AddAsync(category);
                await db.SaveChangesAsync();
                return Results.Ok(category);
            }).WithName("CreateCategory").WithOpenApi();

            app.MapPut("/category/{id}", async (int id, Category category, DataContext db) =>
            {
                var toUpdateCat = await db.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
                if (toUpdateCat is null)
                {
                    return Results.NotFound("No category found");
                }
                toUpdateCat.CategoryName = category.CategoryName;
                toUpdateCat.DeleteFlag = category.DeleteFlag;
                db.Entry(toUpdateCat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Results.Ok();
            }).WithName("UpdateCategory").WithOpenApi();

            app.MapDelete("/category/{id}", async (int id, DataContext db) =>
            {
                var category = await db.Categories.FirstOrDefaultAsync(x=> x.CategoryId == id);    
                if(category is null)
                {
                    return Results.NotFound("No category found");
                }
                db.Entry(category).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return Results.Ok("Category Deleted Successfully");
            });
            return app;
        }

    }
}
