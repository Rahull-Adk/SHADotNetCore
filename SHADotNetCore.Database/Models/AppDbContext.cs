using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SHADotNetCore.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tb1Blog> Tb1Blogs { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString =
   "Data Source=RAHULL;Initial Catalog=DotnetTrainingBatch5;User ID=sa;Password=Rahulltheprogrammer06!;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tb1Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tb1_Blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
