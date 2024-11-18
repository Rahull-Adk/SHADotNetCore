
namespace SHADotNetCore.MinimalApi.Endpoints.Blog
{
    public static class BlogEndpoint
    {
        public static void MapBlogEndpoint(this IEndpointRouteBuilder app)
        {

            app.MapGet("/blogs", () =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.Tb1Blogs.AsNoTracking().ToList();
                return Results.Ok(item);

            }).WithName("GetBlogs").WithOpenApi();

            app.MapGet("/blogs/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.Tb1Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                return Results.Ok(item);
            }).WithName("GetBlog").WithOpenApi();

            app.MapPost("/blogs", (Tb1Blog blog) =>
            {
                AppDbContext db = new AppDbContext();
                db.Tb1Blogs.Add(blog);
                db.SaveChanges();
                return Results.Ok(blog);
            }).WithName("CreateBlog").WithOpenApi();

            app.MapPut("/blogs/{id}", (int id, Tb1Blog blog) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.Tb1Blogs.FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.BadRequest("No data found");
                }
                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Results.Ok(blog);
            }).WithName("UpdateBlog").WithOpenApi();

            app.MapDelete("/blogs/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.Tb1Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.BadRequest("No data found");
                }
                db.Entry(item).State = EntityState.Deleted;
                db.SaveChanges();

                return Results.Ok("Data deleted successfully");
            }
            ).WithName("DeleteBlog").WithOpenApi();
        }
    }

}
