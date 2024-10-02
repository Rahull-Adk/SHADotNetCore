using Microsoft.EntityFrameworkCore;
using SHADotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHADotNetCore.ConsoleApp
{
    internal class AppDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString =
     "Data Source=RAHULL;Initial Catalog=DotnetTrainingBatch5;User ID=sa;Password=Rahulltheprogrammer06!;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
