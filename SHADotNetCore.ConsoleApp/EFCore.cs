using Microsoft.EntityFrameworkCore;
using SHADotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SHADotNetCore.ConsoleApp
{
    public class EFCore
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var list = db.Blogs.Where(x => x.DeleteFlag == false).ToList();
            foreach (var item in list)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title, string author, string content)

        {
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Saving Success" : "Saving failed");
        }

        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            var items = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (items is null)
            {
                Console.WriteLine("No data found");
            }
            Console.WriteLine(items.BlogId);
            Console.WriteLine(items.BlogTitle);
            Console.WriteLine(items.BlogAuthor);
            Console.WriteLine(items.BlogContent);
        }

        public void Update(int id, string title, string author, string content)
        {
            AppDbContext db = new AppDbContext();
            var items = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (items is null)
            {
                Console.WriteLine("No Data found");
                return;
            }
            if (!string.IsNullOrEmpty(title))
            {
                items.BlogTitle = title;
            }
            if (string.IsNullOrEmpty(author))
            {
                items.BlogAuthor = author;
            }
            if (string.IsNullOrEmpty(content))
            {
                items.BlogContent = content;
            }
            db.Entry(items).State = EntityState.Modified;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Updating successful" : "Saving failed");
        }

        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var items = db.Blogs.AsNoTracking().FirstOrDefault(y => y.BlogId == id);
            if (items is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            db.Entry(items).State = EntityState.Deleted;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Delete Successful" : "Deleting Failed");
        }
    }

}
