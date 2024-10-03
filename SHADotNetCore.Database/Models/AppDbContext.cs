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

        => optionsBuilder.UseSqlServer("Server=RAHULL;Database=DotnetTrainingBatch5;User Id=sa;Password=Rahulltheprogrammer06!;TrustServerCertificate=True;");

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
