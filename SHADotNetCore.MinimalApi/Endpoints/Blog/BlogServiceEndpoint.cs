
using SHADotNetCore.Domin.Features.Blog;

namespace SHADotNetCore.MinimalApi.Endpoints.Blog
{
    public static class BlogServiceEndPoint
    {
        public static void UseBlogServiceEndPoint(this IEndpointRouteBuilder app)
        {

                BlogService service = new BlogService();
            app.MapGet("/blogs", () =>
            {
                return service.GetBlogs();

            }).WithName("GetBlogs").WithOpenApi();

            app.MapGet("/blogs/{id}", (int id) =>
            {
                
                var item = service.GetBlog(id);
                return Results.Ok(item);
            }).WithName("GetBlog").WithOpenApi();

            app.MapPost("/blogs", (Tb1Blog blog) =>
            {
                service.CreateBlog(blog);
                return Results.Ok(blog);
            }).WithName("CreateBlog").WithOpenApi();

            app.MapPut("/blogs/{id}", (int id, Tb1Blog blog) =>
            {
                service.UpdateBlog(id, blog);
                return Results.Ok(blog);
            }).WithName("UpdateBlog").WithOpenApi();

            app.MapDelete("/blogs/{id}", (int id) =>
            {
                service.DeleteBlog(id);

                return Results.Ok("Data deleted successfully");
            }
            ).WithName("DeleteBlog").WithOpenApi();
        }
    }

}
