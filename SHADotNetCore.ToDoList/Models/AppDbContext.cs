using Microsoft.EntityFrameworkCore;
using SHADotNetCore.Database.Models;

namespace SHADotNetCore.ToDoList.Models
{
    public partial class AppDbContext: DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }

         public DbSet<Task> Tasks { get; set; }
         public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.TaskId);

                entity.ToTable("ToDoList");

                entity.Property(e => e.TaskTitle).HasMaxLength(255);
                entity.Property(e => e.TaskStatus).HasMaxLength(50);
            });



            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }

}

