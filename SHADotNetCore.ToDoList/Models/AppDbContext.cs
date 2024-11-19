using Microsoft.EntityFrameworkCore;

namespace SHADotNetCore.ToDoList.Models
{
    public partial class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }

         public DbSet<Task> Tasks { get; set; }
         public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().ToTable("Tasks");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }

}

