using Microsoft.EntityFrameworkCore;
using Assessment.Models;

public class AppDbContext : DbContext
{
    public DbSet<TaskManagement> Tasks { get; set; }

    public DbSet<User>? Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
