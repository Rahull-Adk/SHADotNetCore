using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SHADotNetCore.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHADotNetCore.Domin.Features.Blog
{
    public class BlogService
    {
        private readonly AppDbContext _db = new AppDbContext();
        public List<Tb1Blog> GetBlogs()
        {
            var model = _db.Tb1Blogs.AsNoTracking().ToList();
            return model;
        }

        public Tb1Blog GetBlog(int id)
        {
            var item = _db.Tb1Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            return item;
        }


        public Tb1Blog CreateBlog(Tb1Blog blog)
        {
            _db.Tb1Blogs.Add(blog);
            _db.SaveChanges();
            return blog;
        }

        public Tb1Blog UpdateBlog(int id, Tb1Blog blog)
        {
            var item = _db.Tb1Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return null;
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }

        public Tb1Blog PatchBlog(int id, Tb1Blog blog)
        {
            var item = _db.Tb1Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }
        public bool? DeleteBlog(int id)


        {
            var item = _db.Tb1Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return false;
            }
            _db.Entry(item).State = EntityState.Deleted;
            int result = _db.SaveChanges();
            return result > 0;

        }
    }
}
