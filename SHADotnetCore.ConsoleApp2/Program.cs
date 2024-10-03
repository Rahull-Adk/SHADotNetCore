using SHADotNetCore.Database.Models;

Console.WriteLine("Hello, World!");

AppDbContext db = new AppDbContext();
var list = db.Tb1Blogs.ToList();