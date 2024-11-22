using Microsoft.EntityFrameworkCore;
using SHADotNetCore.MiniKBZPay.Endpoints.User;

namespace SHADotNetCore.MiniKBZPay.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<UserModel> Users { get; set; }
}
