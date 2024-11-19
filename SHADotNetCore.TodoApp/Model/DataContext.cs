using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SHADotNetCore.TodoApp.Model
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
            
        }
        public DbSet<TaskList> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
