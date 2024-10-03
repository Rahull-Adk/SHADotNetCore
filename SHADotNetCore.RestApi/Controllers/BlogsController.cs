using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHADotNetCore.Database.Models;

namespace SHADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _db.Tb1Blogs.AsNoTracking().ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _db.Tb1Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlog(Tb1Blog blog)
        {
            _db.Tb1Blogs.Add(blog);
            _db.SaveChanges();
            return Ok(blog);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, Tb1Blog blog)
        {
            var item = _db.Tb1Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null) return NotFound();
            item.BlogTitle = blog.BlogTitle;

            item.BlogTitle = blog.BlogAuthor;

            item.BlogTitle = blog.BlogContent;


            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, Tb1Blog blog)
        {
            var item = _db.Tb1Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null) return NotFound();
            if (string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;

            }
            if (string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogTitle = blog.BlogAuthor;

            }
            if (string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogTitle = blog.BlogContent;

            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = _db.Tb1Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null) return NotFound();
            item.DeleteFlag = true;
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            return Ok();
        }
    }
}
