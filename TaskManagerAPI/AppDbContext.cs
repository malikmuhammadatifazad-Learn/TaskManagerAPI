using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Controllers;

namespace TaskManagerAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }
    }
}